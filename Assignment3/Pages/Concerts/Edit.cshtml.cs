using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment3.Data;
using Assignment3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Assignment3.Pages.Concerts
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext database;
        private readonly UserManager<IdentityUser> _userManager;
        public EditModel(AppDbContext database, UserManager<IdentityUser> userManager)
        {
            this.database = database;
            _userManager = userManager;
        }

        public Concert Concert { get; set; }
        public List<Artist> Artists { get; set; }
        public IdentityUser HttpUser { get; set; }
        public string Message { get; set; }
        public UserGoingConcert UserGoing { get; set; }
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
        public async Task OnGet(int id)
        {
            HttpUser = await _userManager.GetUserAsync(HttpContext.User);

            Concert = await database.Concerts.Include(a => a.Artists)
                     .FirstOrDefaultAsync(c => c.ID == id);
            Artists = Concert.Artists;
            UserGoing = await database.UserGoingConcerts
                .FirstOrDefaultAsync(a => a.User == HttpUser && a.Concert == Concert);


            ArtistOptions = new SelectList(database.Artists.OrderBy(a => a.Name), nameof(Artist.ID), nameof(Artist.Name));
            CityOptions = new SelectList(database.Cities.OrderBy(c => c.Name), nameof(City.ID), nameof(City.Name));
        }

        public async Task<IActionResult> OnPostEdit(Concert concert, int id)
        {
            Concert = await database.Concerts.Include(a => a.Artists).FirstOrDefaultAsync(c => c.ID == id);
            Artists = Concert.Artists;
            ArtistOptions = new SelectList(database.Artists.OrderBy(a => a.Name), nameof(Artist.ID), nameof(Artist.Name));
            CityOptions = new SelectList(database.Cities.OrderBy(c => c.Name), nameof(City.ID), nameof(City.Name));

            var artist1 = await database.Artists.Include(c => c.Concerts).FirstOrDefaultAsync(a => a.ID == SelectedArtist1ID);
            var artist2 = await database.Artists.Include(c => c.Concerts).FirstOrDefaultAsync(a => a.ID == SelectedArtist2ID);
            var artist3 = await database.Artists.Include(c => c.Concerts).FirstOrDefaultAsync(a => a.ID == SelectedArtist3ID);

            Concert.Name = concert.Name;
            Concert.Date = concert.Date;

            Concert.Artists.Clear();
            await database.SaveChangesAsync();

            var concertAdd = await database.Concerts.FirstOrDefaultAsync(c => c.ID == Concert.ID);

            if (artist1 != null) artist1.Concerts.Add(concertAdd);
            if (artist2 != null) artist2.Concerts.Add(concertAdd);
            if (artist3 != null) artist3.Concerts.Add(concertAdd);

            await database.SaveChangesAsync();

            return RedirectToPage("/Concerts/Edit", new { id });
        }

        public async Task<IActionResult> OnPostGoing(int id)
        {
            HttpUser = await _userManager.GetUserAsync(HttpContext.User);

            Concert = await database.Concerts.Include(a => a.Artists)
                    .FirstOrDefaultAsync(c => c.ID == id);
            UserGoing = await database.UserGoingConcerts
                 .FirstOrDefaultAsync(a => a.User == HttpUser && a.Concert == Concert);

            if (UserGoing != null)
            {
                database.UserGoingConcerts.Remove(UserGoing);
            }
            else
            {
                var userGoing = new UserGoingConcert();
                userGoing.Concert = Concert;
                userGoing.User = HttpUser;
                database.UserGoingConcerts.Add(userGoing);
            }

            await database.SaveChangesAsync();

            return RedirectToPage("/Concerts/Edit", new { id });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            Concert = await database.Concerts.Include(a => a.Artists)
                    .FirstOrDefaultAsync(c => c.ID == id);
            UserGoing = await database.UserGoingConcerts
                    .FirstOrDefaultAsync(a => a.Concert == Concert);

            Message = Concert.Name + " has been deleted";

            if (UserGoing != null)
            {
                database.UserGoingConcerts.Remove(UserGoing);
            }

            database.Concerts.Remove(Concert);
            await database.SaveChangesAsync();

            return RedirectToPage("./Index", new { Message });
        }
    }
}
