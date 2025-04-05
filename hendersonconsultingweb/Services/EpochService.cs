namespace HendersonConsulting.Web.Services;

public class EpochService : IEpochService
{
    /// <inheritdoc />
    public long ConvertToUnixTimeMilliseconds(DateTime dateTime) => dateTime.ToUnixTimeMillisecondsAtMidnight();

    /// <inheritdoc />
    public long GetCurrentEpoch() => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

    /// <inheritdoc />
    public long GetCurrentEpochAtMidnight() => new DateTimeOffset(DateTime.UtcNow.Date).ToUnixTimeMilliseconds();
}