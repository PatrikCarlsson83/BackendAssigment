using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment3.Data;
using Assignment3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Assignment3.Pages.Concerts
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext database;
        public IndexModel(AppDbContext database)
        {
            this.database = database;
        }

        [BindProperty(SupportsGet = true)]
        public string Message { get; set; }
        public SelectList Options { get; set; }
        [BindProperty]
        public int SelectedCityID { get; set; }
        public City City { get; set; }
        public List<Concert> NearbyConcerts { get; set; } = new List<Concert>();

        public void OnGet()
        {
            Options = new SelectList(database.Cities.OrderBy(c => c.Name), nameof(City.ID), nameof(City.Name));
        }

        public async Task OnPostTownAsync()
        {
            Options = new SelectList(database.Cities.OrderBy(c => c.Name), nameof(City.ID), nameof(City.Name));

            NearbyConcerts = await database.Concerts.Include(c => c.City)
                .Where(c => c.City.ID == SelectedCityID)
                .OrderBy(c => c.Date)
                .ToListAsync();
        }

        public async Task OnPostFindAsync(City city)
        {
            Options = new SelectList(database.Cities.OrderBy(c => c.Name), nameof(City.ID), nameof(City.Name));

            var concerts = await database.Concerts.Include(c => c.City)
                .OrderBy(a => a.Date)
                .ToListAsync();

            foreach (var concert in concerts)
            {
                var distance = Geolocation.Distance(city, concert.City);
                if (distance <= 100000)
                {
                    NearbyConcerts.Add(concert);
                }
            }
        }
    }
}
