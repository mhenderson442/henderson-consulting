namespace HendersonConsulting.Web.Extensions;

/// <summary>
/// Extension methods for registering services in the application.
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    /// Registers all the scoped services in the application.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> services</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection RegisterScopedServices(this IServiceCollection services)
    {
        services.AddScoped<IEpochService, EpochService>();
        services.AddScoped<IIpInfoService, IpInfoService>();

        return services;
    }

    /// <summary>
    /// Registers all the singleton services in the application.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> services</param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection RegisterSingletonServices(this IServiceCollection services)
    {
        services.AddSingleton<IBuildInfoService, BuildInfoService>();

        return services;
    }

    /// <summary>
    /// Registers all the configuration settings in the application.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="configurationManager"><see cref="ConfigurationManager"/></param>
    /// <returns></returns>
    public static IServiceCollection RegisterApplicationSettings(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        var applicationSettingsSection = configurationManager.GetSection(nameof(ApplicationSettings));

        services.Configure<ApplicationSettings>(applicationSettingsSection);

        return services;
    }

    public static IServiceCollection RegisterHttpClients(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        var ipApiUri = configurationManager.GetValue<string>("ApplicationSettings:IpApiUri");
        var googleRecaptchaValidationUri = configurationManager.GetValue<string>("ApplicationSettings:GoogleRecaptchaValidationUri");

        if (string.IsNullOrWhiteSpace(ipApiUri))
        {
            throw new ArgumentNullException(nameof(configurationManager), "IpApiUri cannot be null or empty.");
        }

        services.AddHttpClient(ApplicationConstants.IpApiHttpClient, client =>
        {
            client.BaseAddress = new Uri(ipApiUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        if (string.IsNullOrWhiteSpace(googleRecaptchaValidationUri))
        {
            throw new ArgumentNullException(nameof(configurationManager), "GoogleRecaptchaValidationUri cannot be null or empty.");
        }

        services.AddHttpClient(ApplicationConstants.GoogleRecaptchaHttpClient, client =>
        {
            client.BaseAddress = new Uri(googleRecaptchaValidationUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        return services;
    }
}