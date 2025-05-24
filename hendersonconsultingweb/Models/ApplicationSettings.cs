namespace HendersonConsulting.Web.Models;

/// <summary>
/// Application settings model.
/// </summary>
public class ApplicationSettings
{
    /// <summary>
    /// Gets or sets the Googl eRecaptchaValidation Uri
    /// </summary>
    public required string GoogleRecaptchaValidationUri { get; init; }

    /// <summary>
    /// Gets or sets the GoogleSiteKey.
    /// </summary>
    public required string GoogleSiteKey { get; init; }

    /// <summary>
    /// Gets or sets the GoogleSiteSecret.
    /// </summary>
    public required string GoogleSiteSecret { get; init; }

    /// <summary>
    /// Gets or sets the IpApiKey.
    /// </summary>
    public required string IpApiKey { get; init; }

    /// <summary>
    /// Gets or sets the IpApiUri.
    /// </summary>
    public required string IpApiUri { get; init; }
}