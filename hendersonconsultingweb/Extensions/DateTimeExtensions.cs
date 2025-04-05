namespace HendersonConsulting.Web.Extensions;

public static class DateTimeExtensions
{
    /// <summary>
    /// Converts a Unix timestamp in milliseconds to a human-readable date and time string.
    /// </summary>
    /// <param name="unixTimestamp"></param>
    /// <returns></returns>
    public static string ToHumanReadableDateTime(this long unixTimestamp)
    {
        var dateTime = DateTimeOffset.FromUnixTimeMilliseconds(unixTimestamp).DateTime;
        return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
    }

    /// <summary>
    /// Converts a DateTime to a Unix timestamp in milliseconds at midnigt of the submitted date.
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public static long ToUnixTimeMillisecondsAtMidnight(this DateTime dateTime)
    {
        var midnightDateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, dateTime.Kind);
        return new DateTimeOffset(midnightDateTime).ToUnixTimeMilliseconds();
    }
}