using Microsoft.AspNetCore.Builder;

namespace HendersonConsulting.Web.Middleware
{
    public static class BlobFileHandlerExtensions
    {
        public static IApplicationBuilder UseBlobFileHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BlobFileHandler>();
        }
    }
}
