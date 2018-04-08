using HendersonConsulting.Web.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace HendersonConsulting.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IStorageRepository _storageRepository;
        
        public string PageContent { get; set; }

        public IndexModel(IStorageRepository storageRepository)
        {
            _storageRepository = storageRepository;
        }

        public async Task OnGetAsync()
        {
            var blogPostContent = await _storageRepository.GetDefaultPostItemAsync();
            PageContent = blogPostContent.PageContent;
        }
    }
}