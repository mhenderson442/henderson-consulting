namespace HendersonConsulting.Web.Models.ApiInfo;

/// <summary>
/// Represents the response from the IP API.
/// </summary>
public class IpApiResponse
{
    [JsonPropertyName("location")]
    public Location? Location { get; set; }
}