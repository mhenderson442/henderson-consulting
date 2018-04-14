using HendersonConsulting.Web.Models;
using HendersonConsulting.Web.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HendersonConsulting.Web.Pages.Archives.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IStorageRepository _storageRepository;

        public List<Category> Categories { get; set; }

        public IndexModel(IStorageRepository storageRepository)
        {
            _storageRepository = storageRepository;
        }

        public async Task OnGetAsync()
        {
            Categories = await _storageRepository.GetCategoriesAsync();
        }
    }
}