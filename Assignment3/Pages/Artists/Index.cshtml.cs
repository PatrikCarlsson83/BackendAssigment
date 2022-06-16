using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Assignment3.Data;
using Assignment3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment3.Pages.Artists
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext database;

        public IndexModel(AppDbContext database)
        {
            this.database = database;
        }

        public Artist Artist { get; set; }
        public List<Artist> Artists { get; set; }
        public List<LikedArtist> Liked { get; set; }
        public string UserID { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Message { get; set; }
        public async Task OnGetAsync()
        {
            Artists = await database.Artists
                .AsNoTracking()
                .OrderBy(a => a.Name)
                .ToListAsync();

            Liked = await database.LikedArtists
                .Include(a => a.Artist)
                .Include(u => u.User)
                .OrderBy(a => a.Artist).ToListAsync();
            UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task<IActionResult> OnPostAsync(Artist artist)
        {

            if (artist != null)
            {
                var newArtist = new Artist
                {
                    Name = artist.Name
                };

                await database.Artists.AddAsync(newArtist);
                await database.SaveChangesAsync();
            }
            return RedirectToPage("./Index");

        }
    }
}
