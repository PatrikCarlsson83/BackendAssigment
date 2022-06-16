using System.Threading.Tasks;
using Assignment3.Data;
using Assignment3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment3.Pages.Concerts
{
    public class CitiesModel : PageModel
    {

        private readonly AppDbContext database;
        public CitiesModel(AppDbContext database)
        {
            this.database = database;
        }
        
        public City City { get; set; }
        public string Message { get; set; }

        public async Task<IActionResult> OnPostAsync(City city)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var newCity = new City();
            newCity.Name = city.Name;
            newCity.Latitude = city.Latitude;
            newCity.Longitude = city.Longitude;

            await database.Cities.AddAsync(newCity);
            await database.SaveChangesAsync();

            Message = city.Name + " has been added to the database.";

            return RedirectToPage("/Concerts/Index", new { Message });
        }
    }
}