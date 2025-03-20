// See https://aka.ms/new-console-template for more information
using Fortune.Models.Configs;
using Fortune.Repositories.Interfaces;
using Fortune.Repositories.MongoDB;
using Fortune.Repositories;
using Fortune.Services;
using Fortune.Services.Interfaces;
using Fortune.Shared.Services.Interfaces;
using Fortune.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using Microsoft.Extensions.Http;
using MongoDB.Driver;
using Fortune.Models.Enums;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        var env = context.HostingEnvironment;

        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

        config.AddUserSecrets<Program>();

        config.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        IConfiguration configuration = context.Configuration;

        // Extract the API key from the configuration
        string openAiApiKey = configuration["ChatGptKey"];
        string ideogramApiKey = configuration["IdeogramKey"];
        string textProvider = configuration["TextProvider"];
        string imageProvider = configuration["ImageProvider"];
        string dbProvider = configuration["DbProvider"];


        services.AddHttpClient<ChatGptService>();
        services.Configure<LuckyNumberConfig>(configuration.GetSection("LuckyNumbers"));
        services.Configure<TtsConfig>(configuration.GetSection("TtsConfig"));


        services.AddSingleton<IBaseAiService, BaseAiService>();
        services.AddSingleton<IFortuneService, FortuneService>();
        services.AddSingleton<ILoggingService, LoggingService>();
        services.AddSingleton<IFortuneRepository, MongoDBRepository>();



        services.AddSingleton<ITtsService>(sp =>
        {
            // Retrieve the TtsConfig instance from the DI container
            var ttsConfig = sp.GetRequiredService<IOptions<TtsConfig>>().Value;

            switch (ttsConfig.TtsProvider?.ToUpper())
            {
                case "ELEVENLABS":
                    // Pass ttsConfig to the ElevenLabsTtsService
                    return new ElevenlabsTtsService(ttsConfig);

                default:
                    throw new InvalidOperationException($"Unsupported TTS provider: {ttsConfig.TtsProvider}");
            }
        });


        // Inject the API key and register the ChatGptService with DI
        switch (textProvider.ToUpper())
        {
            case "OPENAI":
            default:
                services.AddSingleton<IExternalTextAiService, ChatGptService>(sp =>
                {
                    var httpClient = sp.GetRequiredService<HttpClient>();
                    return new ChatGptService(httpClient, openAiApiKey);
                });
                break;
        }

        switch (imageProvider.ToUpper())
        {
            case "IDEOGRAM":
                services.AddSingleton<IExternalImageAiService, IdeogramService>(sp =>
                {
                    var httpClient = sp.GetRequiredService<HttpClient>();
                    return new IdeogramService(httpClient, ideogramApiKey);
                });
                break;
            case "OPENAI":
            default:
                services.AddSingleton<IExternalImageAiService, ChatGptService>(sp =>
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

                services.Configure<MongoDbSettings>(
                configuration.GetSection("MongoDbSettings"));

                services.AddSingleton<MongoDbContext>();

                services.AddSingleton<IFortuneRepository, MongoDBRepository>(sp =>
                {
                    var context = sp.GetRequiredService<MongoDbContext>();
                    var loggingService = sp.GetRequiredService<ILoggingService>();
                    return new MongoDBRepository(context, loggingService);
                });
                break;
        }

        services.AddSingleton<IQrService, QrService>(qr =>
        {
            string siteUrl = configuration["SiteUrl"];
            return new QrService(siteUrl);
        });


    });

var host = builder.Build();

using (var serviceScope = host.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    var fortuneService = services.GetRequiredService<IFortuneService>();

    try
    {
        var command = args.FirstOrDefault() ?? "default";
        switch (command.ToUpper())
        {
            case "SHORTFORM":
                await fortuneService.CreateNewFortunes(1, EFortuneType.ShortForm);
                var fortune = (await fortuneService.GetFortunes(1)).FirstOrDefault();
                return;
            default:
                Console.WriteLine("Default");
                return;
        }
    }
    catch (Exception ex)
    {

    }
}

await host.RunAsync();

