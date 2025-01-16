using Fortune.Models.Configs;
using Fortune.Services;
using Fortune.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Load User Secrets in Development environment
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Extract the API key from the configuration
string openAiApiKey = builder.Configuration["ChatGptKey"];
string ideogramApiKey = builder.Configuration["IdeogramKey"];
string textProvider = builder.Configuration["TextProvider"];
string imageProvider = builder.Configuration["ImageProvider"];
builder.Services.AddHttpClient<ChatGptService>();
builder.Services.Configure<LuckyNumberConfig>(builder.Configuration.GetSection("LuckyNumbers"));
builder.Services.Configure<TtsConfig>(builder.Configuration.GetSection("TtsConfig"));


builder.Services.AddSingleton<ITtsService>(sp => {
    // Retrieve the TtsConfig instance from the DI container
    var ttsConfig = sp.GetRequiredService<IOptions<TtsConfig>>().Value;

    switch (ttsConfig.TtsProvider?.ToUpper()) {
        case "ELEVENLABS":
            // Pass ttsConfig to the ElevenLabsTtsService
            return new ElevenlabsTtsService(ttsConfig);

        default:
            throw new InvalidOperationException($"Unsupported TTS provider: {ttsConfig.TtsProvider}");
    }
});


// Inject the API key and register the ChatGptService with DI
switch (textProvider.ToUpper()) {
    case "OPENAI":
    default:
        builder.Services.AddSingleton<IExternalTextAiService, ChatGptService>(sp =>
        {
            var httpClient = sp.GetRequiredService<HttpClient>();
            return new ChatGptService(httpClient, openAiApiKey);
        });
        break;
}

switch (textProvider.ToUpper())
{
    case "IDEOGRAM":
        builder.Services.AddSingleton<IExternalImageAiService, IdeogramService>(sp =>
        {
            var httpClient = sp.GetRequiredService<HttpClient>();
            return new IdeogramService(httpClient, ideogramApiKey);
        });
        break;
    case "OPENAI":
    default:
        builder.Services.AddSingleton<IExternalImageAiService, ChatGptService>(sp =>
        {
            var httpClient = sp.GetRequiredService<HttpClient>();
            return new ChatGptService(httpClient, openAiApiKey);
        });
        break;
}

builder.Services.AddSingleton<IQrService, QrService>(qr =>
{
    string siteUrl = builder.Configuration["SiteUrl"];
    return new QrService(siteUrl);
});


builder.Services.AddSingleton<IAiService, AiService>();
builder.Services.AddSingleton<IFortuneService, FortuneService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
