namespace HendersonConsulting.Web.Pages
{
    using System.Threading.Tasks;
    using HendersonConsulting.Web.Repositories;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    /// <summary>
    /// Page model for post. <see cref="PostModel"/>
    /// </summary>
    public class PostModel : PageModel
    {
        /// <summary>
        /// <see cref="IStorageRepository"/> instance.
        /// </summary>
        private readonly IStorageRepository _storageRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PostModel" /> class.
        /// </summary>
        /// <param name="storageRepository"><see cref="IStorageRepository"/> instance.</param>
        public PostModel(IStorageRepository storageRepository)
        {
            _storageRepository = storageRepository;
        }

        /// <summary>
        /// Gets or sets <see cref="Year" />.
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Month" />.
        /// </summary>
        public string Month { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Day" />.
        /// </summary>
        public string Day { get; set; }

        /// <summary>
        /// Gets or sets <see cref="Name" />.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets <see cref="DatePosted" />.
        /// </summary>
        public string DatePosted { get; set; }

        /// <summary>
        /// Gets or sets <see cref="PageContent" />.
        /// </summary>
        public string PageContent { get; set; }

        /// <summary>
        /// OnGetAsync method for <see cref="PostModel"/>.
        /// </summary>
        /// <param name="year">year string instance.</param>
        /// <param name="month">month string instance.</param>
        /// <param name="day">day string instance.</param>
        /// <param name="name">name string instance.</param>
        /// <returns>Returns the <see cref="OnGetAsync"/> instance.</returns>
        public async Task OnGetAsync(string year, string month, string day, string name)
        {
            Year = year;
            Month = month;
            Day = day;
            Name = name;

            var blogPostContent = await _storageRepository.GetBlogPostItemAsync(year, month, day, name);
            DatePosted = blogPostContent.DatePosted;
            PageContent = blogPostContent.PageContent;
        }
    }
}