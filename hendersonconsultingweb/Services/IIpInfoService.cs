namespace HendersonConsulting.Web.Services;

/// <summary>
/// Interface for the IpInfoService.
/// </summary>
public interface IIpInfoService
{
    /// <summary>
    /// Retrieves the location information based on the provided IP address asynchronously.
    /// </summary>
    /// <param name="ipAddress"><see cref="string"/> representing IP address to lookip</param>
    /// <returns>An instance of <see cref="Location"/></returns>
    Task<Location> GetLocationByIpAddressAsync(string ipAddress);
}