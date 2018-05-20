namespace HendersonConsulting.Web.Middleware
{
    using Microsoft.AspNetCore.Builder;

    /// <summary>
    /// Middleware class for wiring up middleware. <see cref="BlobFileHandlerExtensions"/>
    /// </summary>
    public static class BlobFileHandlerExtensions
    {
        /// <summary>
        /// Builder method for wiring up middleware.
        /// </summary>
        /// <param name="builder"><see cref="IApplicationBuilder"/> instance.</param>
        /// <returns>Returns the <see cref="Task"/></returns>
        public static IApplicationBuilder UseBlobFileHandler(this IApplicationBuilder builder)
        {
            var applicaitonBuilder = builder.UseMiddleware<BlobFileHandler>();

            return applicaitonBuilder;
        }
    }
}
