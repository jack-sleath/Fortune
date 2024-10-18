using Fortune.Models.Configs;
using Fortune.Services;
using Fortune.Services.Interfaces;

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
builder.Services.Configure<LuckyNumberConfig>(builder.Configuration.GetSection("LuckyNumbers"));

// Register the HttpClient for ChatGptService
builder.Services.AddHttpClient<ChatGptService>();

// Inject the API key and register the ChatGptService with DI
builder.Services.AddSingleton<IExternalTextAiService, ChatGptService>(sp =>
{
    var httpClient = sp.GetRequiredService<HttpClient>();
    return new ChatGptService(httpClient, openAiApiKey);
});

builder.Services.AddSingleton<IExternalImageAiService, ChatGptService>(sp =>
{
    var httpClient = sp.GetRequiredService<HttpClient>();
    return new ChatGptService(httpClient, openAiApiKey);
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
