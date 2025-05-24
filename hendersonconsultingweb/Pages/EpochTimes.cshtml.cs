namespace HendersonConsulting.Web.Pages;

public class EpochTimesModel : PageModel
{
    private readonly IEpochService _epochService;
    private readonly ApplicationSettings _appSettings;

    public EpochTimesModel(IOptions<ApplicationSettings> appSettings, IEpochService epochService)
    {
        _epochService = epochService ?? throw new ArgumentNullException(nameof(epochService));
        _appSettings = appSettings.Value;

        var currentEpoch = _epochService.GetCurrentEpoch();

        CurrentEpochTime = currentEpoch;
        CurrentEpochTimeString = currentEpoch.ToHumanReadableDateTime();
    }

    /// <summary>
    /// Gets the Google Site Key.
    /// </summary>
    public string GoogleSiteKey => _appSettings.GoogleSiteKey;

    /// <summary>
    /// Gets or sets the current Unix timestamp.
    /// </summary>
    public required long CurrentEpochTime { get; set; }

    /// <summary>
    /// Gets or sets the current Unix timestamp as a string.
    /// </summary>
    public required string CurrentEpochTimeString { get; set; }

    /// <summary>
    /// Gets or sets the converted Unix timestamp.
    /// </summary>
    public long? ConvertedTimestamp { get; set; }

    /// <summary>
    /// Gets or sets the date and time input from the form.
    /// </summary>
    [BindProperty]
    [Required(ErrorMessage = "DateTime input is required.")]
    public DateTime? DateTimeInput { get; set; }

    /// <summary>
    /// Gets the date and time input as a string formatted for display.
    /// </summary>
    public string? DateTimeInputString => DateTimeInput?.ToString("yyyy-MM-dd HH:mm:ss");

    /// <summary>
    /// Gets or sets the date input at midnight.
    /// </summary>
    public long? DateInputAtMidnight => DateTimeInput?.ToUnixTimeMillisecondsAtMidnight();

    /// <summary>
    /// Gets the date input at midnight as a string formatted for display.
    /// </summary>
    public string? DateInputAtMidnightString => DateInputAtMidnight?.ToHumanReadableDateTime();

    /// <summary>
    /// Handles the GET request for the page.
    /// </summary>
    public void OnGet()
    {
    }

    /// <summary>
    /// Handles the POST request for the form submission.
    /// </summary>
    public void OnPost()
    {
        if (!ModelState.IsValid)
        {
            return;
        }

        var dateTimeInput = DateTimeInput?.ToUniversalTime() ?? DateTime.UtcNow;

        var convertedEpochTime = _epochService.ConvertToUnixTimeMilliseconds(dateTimeInput);
        ConvertedTimestamp = convertedEpochTime;
    }
}