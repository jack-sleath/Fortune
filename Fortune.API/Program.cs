using Fortune.Models.Configs;
using Fortune.Repositories;
using Fortune.Repositories.Interfaces;
using Fortune.Repositories.MongoDB;
using Fortune.Services;
using Fortune.Services.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;

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
string dbProvider = builder.Configuration["DbProvider"];
builder.Services.Configure<LuckyNumberConfig>(builder.Configuration.GetSection("LuckyNumbers"));

// Register the HttpClient for ChatGptService
builder.Services.AddHttpClient<ChatGptService>();

// Inject the API key and register the ChatGptService with DI
switch (textProvider.ToUpper())
{
    case "OPENAI":
    default:
        builder.Services.AddSingleton<IExternalTextAiService, ChatGptService>(sp =>
        {
            var httpClient = sp.GetRequiredService<HttpClient>();
            return new ChatGptService(httpClient, openAiApiKey);
        });
        break;
}

switch (imageProvider.ToUpper())
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

switch (dbProvider.ToUpper())
{
    case "MONGODB":
    default:
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        builder.Services.Configure<MongoDbSettings>(
        builder.Configuration.GetSection("MongoDbSettings"));

        builder.Services.AddSingleton<MongoDbContext>();

        builder.Services.AddSingleton<IFortuneRepository, MongoDBRepository>(sp =>
        {
            var context = sp.GetRequiredService<MongoDbContext>();
            return new MongoDBRepository(context);
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
builder.Services.AddSingleton<IFortuneRepository, MongoDBRepository>();


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
