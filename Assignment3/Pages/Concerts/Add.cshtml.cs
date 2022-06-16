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
    public class AddModel : PageModel
    {
        private readonly AppDbContext database;

        public AddModel(AppDbContext database)
        {
            this.database = database;
        }

        public Concert Concert { get; set; }
        public string Message { get; set; }
        public SelectList ArtistOptions { get; set; }
        [BindProperty]
        public int SelectedArtist1ID { get; set; }
        [BindProperty]
        public int SelectedArtist2ID { get; set; }
        [BindProperty]
        public int SelectedArtist3ID { get; set; }
        public SelectList CityOptions { get; set; }
        [BindProperty]
        public int SelectedCityID { get; set; }

        public void OnGet()
        {
            ArtistOptions = new SelectList(database.Artists.OrderBy(a => a.Name), nameof(Artist.ID), nameof(Artist.Name));
            CityOptions = new SelectList(database.Cities.OrderBy(c => c.Name), nameof(City.ID), nameof(City.Name));
        }

        public async Task<IActionResult> OnPostAsync(Concert concert)
        {
            var artist1 = await database.Artists.Include(c => c.Concerts)
                .FirstOrDefaultAsync(a => a.ID == SelectedArtist1ID);
            var artist2 = await database.Artists.Include(c => c.Concerts)
                .FirstOrDefaultAsync(a => a.ID == SelectedArtist2ID);
            var artist3 = await database.Artists.Include(c => c.Concerts)
                .FirstOrDefaultAsync(a => a.ID == SelectedArtist3ID);
            var city = await database.Cities.FirstOrDefaultAsync(c => c.ID == SelectedCityID);

            var newConcert = new Concert();
            newConcert.Name = concert.Name;
            newConcert.City = city;
            newConcert.Date = concert.Date;

            await database.Concerts.AddAsync(newConcert);
            await database.SaveChangesAsync();

            var concertArtist = await database.Concerts.FirstOrDefaultAsync(c => c.ID == newConcert.ID);

            if (artist1 != null) artist1.Concerts.Add(concertArtist);
            if (artist2 != null) artist2.Concerts.Add(concertArtist);
            if (artist3 != null) artist3.Concerts.Add(concertArtist);

            await database.SaveChangesAsync();

            Message = "Concert " + newConcert.Name + " added to database";

            return RedirectToPage("/Concerts/Index", new { Message });
        }
    }
}
