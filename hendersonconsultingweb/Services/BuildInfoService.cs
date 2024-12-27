namespace HendersonConsulting.Web.Services;

public class BuildInfoService : IBuildInfoService
{
    public string GetBuildNumber()
    {
        var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        return version?.ToString() ?? "Unknown";
    }
}