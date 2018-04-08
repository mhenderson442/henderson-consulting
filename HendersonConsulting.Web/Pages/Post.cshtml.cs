using HendersonConsulting.Web.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace HendersonConsulting.Web.Pages
{
    public class PostModel : PageModel
    {
        private readonly IStorageRepository _storageRepository;

        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string Name { get; set; }

        public string PageContent { get; set; }

        public PostModel(IStorageRepository storageRepository)
        {
            _storageRepository = storageRepository;
        }

        public async Task OnGetAsync(string year, string month, string day, string name)
        {
            Year = year;
            Month = month;
            Day = day;
            Name = name;

            var blogPostContent = await _storageRepository.GetBlogPostItemAsync(year, month, day, name);
            PageContent = blogPostContent.PageContent;
        }
    }
}