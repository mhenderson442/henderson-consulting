namespace HendersonConsulting.Web.Models.Google;

/// <summary>
/// Represents the result of a reCAPTCHA verification.
/// </summary>
public class RecaptchaVerificationResult
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("challenge_ts")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ChallengeTimestamp { get; set; }

    [JsonPropertyName("hostname")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Hostname { get; set; }

    [JsonPropertyName("score")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? ErrorCodes { get; set; }
}