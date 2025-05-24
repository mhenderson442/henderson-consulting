namespace HendersonConsulting.Web.Extensions;

/// <summary>
/// Extension methods for registering configurationManager in the application.
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    /// Registers all Azure key vault configuration configurationManager in the application.
    /// </summary>
    /// <param name="configurationManager">The <see cref="ConfigurationManager"/></param>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>The <see cref="ConfigurationManager"/></returns>
    public static ConfigurationManager RegisterAzureKeyVault(this ConfigurationManager configurationManager)
    {
        var vaultUri = InitializeVaultUri(configurationManager);
        var credential = new DefaultAzureCredential();

        configurationManager.AddAzureKeyVault(vaultUri, credential);

        return configurationManager;
    }

    /// <summary>
    /// Initializes and returns a <see cref="Uri"/> instance for the Key Vault based on the provided configuration.
    /// </summary>
    /// <param name="configurationManager">The configuration manager used to retrieve the Key Vault URL. Must not be <c>null</c>.</param>
    /// <returns>A <see cref="Uri"/> representing the Key Vault URL.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the Key Vault URL is not configured or is an empty string in the provided <paramref
    /// name="configurationManager"/>.</exception>
    private static Uri InitializeVaultUri(ConfigurationManager configurationManager)
    {
        var keyVaultUrl = configurationManager.GetValue<string>("KeyVaultUrl");

        if (string.IsNullOrEmpty(keyVaultUrl))
        {
            throw new ArgumentNullException(nameof(configurationManager), "Key vault URL cannot be null or empty.");
        }

        return new Uri(keyVaultUrl);
    }
}