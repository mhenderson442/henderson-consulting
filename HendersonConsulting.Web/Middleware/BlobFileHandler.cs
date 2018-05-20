namespace HendersonConsulting.Web.Middleware
{
    using System.Threading.Tasks;
    using HendersonConsulting.Web.Repositories;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Middleware class for getting images from blob storage. <see cref="BlobFileHandler" />
    /// </summary>
    public class BlobFileHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BlobFileHandler" /> class.
        /// </summary>
        /// <param name="next"><see cref="RequestDelegate"/> instance.</param>
        public BlobFileHandler(RequestDelegate next)
        {
        }

        /// <summary>
        /// Gets image form static blob storage.
        /// </summary>
        /// <param name="context"><see cref="HttpContext"/> instance.</param>
        /// <returns>Returns the <see cref="Task"/></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            string itemPath = context.Request.PathBase + context.Request.Path;
            var storageRepository = (IStorageRepository)context.RequestServices.GetService(typeof(IStorageRepository));

            var imageBlob = await storageRepository.GetImageBlobAsych(itemPath.Substring(1));
            await imageBlob.DownloadToStreamAsync(context.Response.Body);
        }
    }
}
