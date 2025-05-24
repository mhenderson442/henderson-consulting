namespace HendersonConsulting.Web.Pages;

public class IpInfoModel(IOptions<ApplicationSettings> appSettings,
                         IIpInfoService ipApiInfoService,
                         IHttpContextAccessor httpContextAccessor) : PageModel
{
    private readonly ApplicationSettings _appSettings = appSettings.Value;
    private readonly IIpInfoService _ipApiInfoService = ipApiInfoService;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    /// <summary>
    /// Gets or sets the IP Address.
    /// </summary>
    [BindProperty]
    [Required(ErrorMessage = "IP Address is required.")]
    [RegularExpression(RegexPatterns.IPv4Address, ErrorMessage = "Invalid IP Address format.")]
    public required string IpAddress { get; set; }

    /// <summary>
    /// Gets the Google Site Key.
    /// </summary>
    public string GoogleSiteKey => _appSettings.GoogleSiteKey;

    /// <summary>
    /// Gets or sets the IP API response.
    /// </summary>
    public Location? Location { get; set; }

    public void OnGet()
    {
        // Retrieve the IP address from the HTTP request

        var ipAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        var isValidIpAddress = Regex.IsMatch(ipAddress ?? string.Empty, RegexPatterns.IPv4Address);

        if (isValidIpAddress && !string.IsNullOrWhiteSpace(ipAddress))
        {
            IpAddress = ipAddress;
        }
    }

    /// <summary>
    /// Handles the POST request for the form submission.
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Process the valid IP Address here.
        Location = await _ipApiInfoService.GetLocationByIpAddressAsync(IpAddress);

        return Page();
    }
}