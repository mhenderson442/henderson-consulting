namespace HendersonConsulting.Web.Services;

public interface IEpochService
{
    /// <summary>
    /// Convert a Unix timestamp to a <see cref="long"/> object.
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns><see cref="long"/> representing a Unix timestamp</returns>
    long ConvertToUnixTimeMilliseconds(DateTime dateTime);

    /// <summary>
    /// Get the current Unix timestamp.
    /// </summary>
    /// <returns><see cref="long"/> representing current Unix timestamp</returns>
    long GetCurrentEpoch();

    /// <summary>
    /// Get the current Unix timestamp at midnight UTC.
    /// </summary>
    /// <returns><see cref="long"/> representing current Unix timestamp at midnight UTC.</returns>
    long GetCurrentEpochAtMidnight();
}