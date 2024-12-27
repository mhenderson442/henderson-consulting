using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HendersonConsulting.Web.Pages;

public class IndexModel() : PageModel
{
    public string YearsExperience => GetYearsExpirenceValue();

    public void OnGet()
    {
    }

    private static string GetYearsExpirenceValue()
    {
        var dateTimeCurrent = DateTime.Now;
        var dateTimeStart = new DateTime(1999, 1, 1, 0, 0, 0);
        var yearsExperience = dateTimeCurrent.Year - dateTimeStart.Year;

        if (dateTimeCurrent < dateTimeStart.AddYears(yearsExperience))
        {
            yearsExperience--;
        }

        return yearsExperience.ToString();
    }
}