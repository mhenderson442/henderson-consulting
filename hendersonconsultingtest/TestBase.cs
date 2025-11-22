namespace HendersonConsulting.Web.Test;

public class TestBase
{
    internal static IOptions<ApplicationSettings> GetApplicationSettings()
    {
        var configuration = BuildConfiguration();
        var applicationSettings = new ApplicationSettings
        {
            GoogleRecaptchaValidationUri = configuration["ApplicationSettings:GoogleRecaptchaValidationUri"]!,
            GoogleSiteKey = configuration["ApplicationSettings:GoogleSiteKey"]!,
            GoogleSiteSecret = configuration["ApplicationSettings:GoogleSiteSecret"]!,
            IpApiKey = configuration["ApplicationSettings:IpApiKey"]!,
            IpApiUri = configuration["ApplicationSettings:IpApiUri"]!
        };

        configuration.GetSection(nameof(ApplicationSettings)).Bind(applicationSettings);

        return Options.Create(applicationSettings);
    }

    internal static ServiceProvider InitiateServiceProvider()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.RegisterScopedServices();

        var configurationManager = InitiateConfigurationManager();
        serviceCollection.AddSingleton<IConfiguration>(configurationManager);
        serviceCollection.RegisterApplicationSettings(configurationManager);
        serviceCollection.RegisterHttpClients(configurationManager);

        return serviceCollection.BuildServiceProvider();
    }

    private static IConfiguration BuildConfiguration()
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();

        var configuration = configurationBuilder.Build();
        var keyVaultUrl = configuration["KeyVaultUrl"];

        if (!string.IsNullOrWhiteSpace(keyVaultUrl))
        {
            configuration = new ConfigurationBuilder()
                .AddConfiguration(configuration)
                .AddAzureKeyVault(
                    new Uri(keyVaultUrl),
                    new DefaultAzureCredential())
                .Build();
        }

        return configuration;
    }

    private static ConfigurationManager InitiateConfigurationManager()
    {
        var configuration = BuildConfiguration();

        var configurationManager = new ConfigurationManager();
        configurationManager.AddConfiguration(configuration);

        return configurationManager;
    }
}