namespace HendersonConsulting.Web.Services;

/// <summary>
/// Service for retrieving location information based on IP address.
/// </summary>
public class IpInfoService(IOptions<ApplicationSettings> options, IHttpClientFactory httpClientFactory) : IIpInfoService
{
    private readonly ApplicationSettings _applicationSettings = options.Value;
    private readonly HttpClient _ipApiHttpClient = httpClientFactory.CreateClient(ApplicationConstants.IpApiHttpClient);

    /// <inheritdoc />
    public async Task<Location> GetLocationByIpAddressAsync(string ipAddress)
    {
        var ipAddressKey = _applicationSettings.IpApiKey;

        // Example of using the injected named HttpClient
        var response = await _ipApiHttpClient.GetAsync($"?apiKey={ipAddressKey}&q={ipAddress}");
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var ipApiResponse = JsonSerializer.Deserialize<IpApiResponse>(jsonResponse);

        var location = ipApiResponse?.Location ?? new Location();

        return location ?? new Location();
    }
}