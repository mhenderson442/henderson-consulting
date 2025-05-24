namespace HendersonConsulting.Web.Utilities;

public static class RegexPatterns
{
    /// <summary>
    /// Regular expression for validating an IPv4 address.
    /// </summary>
    public const string IPv4Address = @"^(25[0-5]|2[0-4][0-9]|[0-1]?[0-9][0-9]?)\." +
                                       @"(25[0-5]|2[0-4][0-9]|[0-1]?[0-9][0-9]?)\." +
                                       @"(25[0-5]|2[0-4][0-9]|[0-1]?[0-9][0-9]?)\." +
                                       @"(25[0-5]|2[0-4][0-9]|[0-1]?[0-9][0-9]?)$";
}