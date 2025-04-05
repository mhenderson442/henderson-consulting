namespace HendersonConsulting.Web.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IEpochService, EpochService>();
        services.AddSingleton<IBuildInfoService, BuildInfoService>();

        return services;
    }
}