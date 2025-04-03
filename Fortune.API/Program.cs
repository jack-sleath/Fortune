using Fortune.Models.Configs;
using Fortune.Repositories;
using Fortune.Repositories.Interfaces;
using Fortune.Repositories.MongoDB;
using Fortune.Services;
using Fortune.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using Microsoft.OpenApi.Models;
using Fortune.Shared.Services.Interfaces;
using Fortune.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Load User Secrets in Development environment
if (builder.Environment.IsDevelopment()) {
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
string dbProvider = builder.Configuration["DbProvider"];
builder.Services.AddHttpClient<ChatGptService>();
builder.Services.Configure<LuckyNumberConfig>(builder.Configuration.GetSection("LuckyNumbers"));
builder.Services.Configure<TtsConfig>(builder.Configuration.GetSection("TtsConfig"));


builder.Services.AddSingleton<IAiService, AiService>();
builder.Services.AddSingleton<ITicketService, TicketService>();
builder.Services.AddSingleton<ILoggingService, LoggingService>();
builder.Services.AddSingleton<IFortuneRepository, MongoDBRepository>();



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
        builder.Services.AddSingleton<IExternalTextAiService, ChatGptService>(sp => {
            var httpClient = sp.GetRequiredService<HttpClient>();
            return new ChatGptService(httpClient, openAiApiKey);
        });
        break;
}

switch (imageProvider.ToUpper()) {
    case "IDEOGRAM":
        builder.Services.AddSingleton<IExternalImageAiService, IdeogramService>(sp => {
            var httpClient = sp.GetRequiredService<HttpClient>();
            return new IdeogramService(httpClient, ideogramApiKey);
        });
        break;
    case "OPENAI":
    default:
        builder.Services.AddSingleton<IExternalImageAiService, ChatGptService>(sp => {
            var httpClient = sp.GetRequiredService<HttpClient>();
            return new ChatGptService(httpClient, openAiApiKey);
        });
        break;
}

switch (dbProvider.ToUpper()) {
    case "MONGODB":
    default:
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        builder.Services.Configure<MongoDbSettings>(
        builder.Configuration.GetSection("MongoDbSettings"));

        builder.Services.AddSingleton<MongoDbContext>();

        builder.Services.AddSingleton<IFortuneRepository, MongoDBRepository>(sp => {
            var context = sp.GetRequiredService<MongoDbContext>();
            var loggingService = sp.GetRequiredService<ILoggingService>();
            return new MongoDBRepository(context, loggingService);
        });
        break;
}

builder.Services.AddSingleton<IQrService, QrService>(qr => {
    string siteUrl = builder.Configuration["SiteUrl"];
    return new QrService(siteUrl);
});

string[] uiUrls = builder.Configuration.GetSection("uiUrls").Get<string[]>();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowSpecificOrigin", policy => {
        policy.WithOrigins(uiUrls)
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowSpecificOrigin");

app.Run();