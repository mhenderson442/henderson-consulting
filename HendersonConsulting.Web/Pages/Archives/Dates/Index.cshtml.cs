namespace HendersonConsulting.Web.Pages.Archives.Dates
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using HendersonConsulting.Web.Models;
    using HendersonConsulting.Web.Repositories;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    /// <summary>
    /// Page model for Dates. <see cref="IndexModel"/>
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
        ///  Gets or sets <see cref="Posts" />.
        /// </summary>
        public List<BlogPostYear> Posts { get; set; }

        /// <summary>
        ///  OnGetAsync method for <see cref="IndexModel"/>.
        /// </summary>
        /// <returns>Returns the <see cref="OnGetAsync"/> instance.</returns>
        public async Task OnGetAsync()
        {
            Posts = await _storageRepository.GetBlogPostListAsync();
        }
    }
}