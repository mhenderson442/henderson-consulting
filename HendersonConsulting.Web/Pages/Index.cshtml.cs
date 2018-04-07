using HendersonConsulting.Web.Models;
using HendersonConsulting.Web.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HendersonConsulting.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IStorageRepository _storageRepository;
        
        public List<BlogPostYear> Posts { get; set; }

        public string PageContent { get; set; }

        public string Title { get; set; }

        public IndexModel(IStorageRepository storageRepository)
        {
            _storageRepository = storageRepository;
        }

        public async Task OnGetAsync()
        {
            Posts = await _storageRepository.GetBlogPostListAsync();

            var blogPostContent = await _storageRepository.GetDefaultPostItemAsync();

            Title = blogPostContent.Title;
            PageContent = blogPostContent.PageContent;
        }
    }
}