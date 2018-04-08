using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace HendersonConsulting.Web.Pages
{
    public class PostModel : PageModel
    {
        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string Name { get; set; }

        public async Task OnGetAsync(string year, string month, string day, string name)
        {
            Year = year;
            Month = month;
            Day = day;
            Name = name;


        }
    }
}