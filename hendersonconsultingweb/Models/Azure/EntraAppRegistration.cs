namespace HendersonConsulting.Web.Models.Azure;

/// <summary>
/// Represents the registration details for an application in Microsoft Entra ID (formerly Azure AD).
/// </summary>
/// <remarks>This class encapsulates the necessary credentials and identifiers required to authenticate and
/// interact with Microsoft Entra ID on behalf of an application.</remarks>
public class EntraAppRegistration
{
    /// <summary>
    /// Gets or sets the unique identifier for the tenant.
    /// </summary>
    public required string TenantId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the client.
    /// </summary>
    public required string ClientId { get; set; }

    /// <summary>
    /// Gets or sets the client secret used for authentication.
    /// </summary>
    public required string ClientSecret { get; set; }
}