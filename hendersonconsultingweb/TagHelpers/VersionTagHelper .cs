using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Reflection;

namespace HendersonConsulting.Web.TagHelpers;

[HtmlTargetElement("app-version")]
public class VersionTagHelper: TagHelper
{
    /// <summary>
    /// Tag healper process to render application version.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="output"></param>
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var version = Assembly
            .GetExecutingAssembly()
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
            .InformationalVersion ?? "Unknown";

        // Split version and commit hash if present
        var parts = version.Split('+');
        var displayVersion = parts[0];
        var commitHash = parts.Length > 1 ? parts[1][..7] : string.Empty;

        // Format output: "1.1.0 (5ca94bb)" if hash exists
        var formattedVersion = string.IsNullOrEmpty(commitHash)
            ? displayVersion
            : $"{displayVersion} ({commitHash})";

        output.TagName = "span"; // Render as <span>
        output.Content.SetContent(formattedVersion);
    }

}

