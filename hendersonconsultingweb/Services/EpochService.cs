namespace HendersonConsulting.Web.Services;

public class EpochService : IEpochService
{
    /// <inheritdoc />
    public long ConvertToUnixTimeMilliseconds(DateTime dateTime) => ((DateTimeOffset)dateTime).ToUnixTimeMilliseconds();

    /// <inheritdoc />
    public long GetCurrentEpoch() => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

    /// <inheritdoc />
    public long GetCurrentEpochAtMidnight() => new DateTimeOffset(DateTime.UtcNow.Date).ToUnixTimeMilliseconds();
}