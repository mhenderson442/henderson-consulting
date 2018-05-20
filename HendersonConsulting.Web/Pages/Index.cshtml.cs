namespace HendersonConsulting.Web.Pages
{
    using System.Threading.Tasks;
    using HendersonConsulting.Web.Repositories;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    /// <summary>
    /// Page model for site. <see cref="IndexModel"/>
    /// </summary>
    public class IndexModel : PageModel
    {
        /// <summary>
        /// <see cref="IStorageRepository"/> instance.
        /// </summary>
        private readonly IStorageRepository _storageRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexModel" /> class.
        /// </summary>
        /// <param name="storageRepository"><see cref="IStorageRepository"/> instance.</param>
        public IndexModel(IStorageRepository storageRepository)
        {
            _storageRepository = storageRepository;
        }

        /// <summary>
        /// Gets or sets <see cref="DatePosted" />.
        /// </summary>
        public string DatePosted { get; set; }

        /// <summary>
        /// Gets or sets <see cref="PageContent" />.
        /// </summary>
        public string PageContent { get; set; }

        /// <summary>
        /// OnGetAsync method for <see cref="IndexModel"/>.
        /// </summary>
        /// <returns>Returns the <see cref="OnGetAsync"/> instance.</returns>
        public async Task OnGetAsync()
        {
            var blogPostContent = await _storageRepository.GetDefaultPostItemAsync();
            DatePosted = blogPostContent.DatePosted;
            PageContent = blogPostContent.PageContent;
        }
    }
}