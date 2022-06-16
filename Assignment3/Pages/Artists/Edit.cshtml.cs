using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Assignment3.Data;
using Assignment3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment3.Pages.Artists
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

        public Artist Artist { get; set; }
        public string UserID { get; set; }
        public LikedArtist Liked { get; set; }
        public List<Album> Albums { get; set; }
        public List<Artist> Artists { get; set; }
        public Album Album { get; set; }
        public string Message { get; set; }

        public async Task OnGetAsync(int id)
        {
            await GetValues(id);
        }

        public async Task<IActionResult> OnPostAddAsync(int id)
        {
            Artist = await database.Artists.FirstOrDefaultAsync(a => a.ID == id);
            UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Liked = await database.LikedArtists
                .FirstOrDefaultAsync(a => a.User.Id == UserID && a.Artist.ID == id);

            if (Liked != null)
            {
                database.LikedArtists.Remove(Liked);
            }
            else
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                LikedArtist liked = new LikedArtist();
                liked.Artist = Artist;
                liked.User = user;
                database.LikedArtists.Add(liked);
            }

            await database.SaveChangesAsync();
            return RedirectToPage("./Edit");
        }

        public async Task<IActionResult> OnPostAddAsync(Album album, int id)
        {
            Artist = await database.Artists.FirstOrDefaultAsync(a => a.ID == id);

            if (album != null)
            {
                var newAlbum = new Album
                {
                    Title = album.Title,
                    Artist = Artist
                };

                await database.Albums.AddAsync(newAlbum);
            }

            await database.SaveChangesAsync();
            return RedirectToPage("./Edit");
        }

        public async Task<IActionResult> OnPostEditAsync(Artist artist, int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Artist = await database.Artists.Include(a => a.Albums)
                .Include(c => c.Concerts)
                .FirstOrDefaultAsync(a => a.ID == id);

            Artist.Name = artist.Name;
            await database.SaveChangesAsync();
            return RedirectToPage("./Edit");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var removeLikes = database.LikedArtists.Include(a => a.Artist)
                .Where(l => l.Artist.ID == id);
            database.LikedArtists.RemoveRange(removeLikes);

            Artist = await database.Artists.Include(a => a.Albums)
                .ThenInclude(s => s.Songs)
                .ThenInclude(p => p.Playlists)
                .FirstOrDefaultAsync(a => a.ID == id);

            Message = Artist.Name + " has been deleted";

            database.Artists.Remove(Artist);
            await database.SaveChangesAsync();

            return RedirectToPage("./Index", new { Message });
        }

        public async Task OnPostTourWith(int id)
        {
            await GetValues(id);
            var concerts = await database.Concerts.Include(c => c.City)
                .Include(a => a.Artists)
                .ThenInclude(a => a.Albums)
                .OrderBy(c => c.Name)
                .ToListAsync();

            var artists = new List<Concert>();
            var touredWith = new List<Artist>();

            foreach (var concert in concerts)
            {
                foreach (var artist in concert.Artists)
                {
                    if (artist.ID == id)
                    {
                        artists.Add(concert);
                    }
                }
            }

            foreach (var concert in artists)
            {
                foreach (var artist in concert.Artists)
                {
                    if (artist.ID == id)
                    {
                        continue;
                    }
                    touredWith.Add(artist);
                }
            }
            var touredWithDistinct = touredWith.Distinct().ToList();

            Artists = touredWithDistinct;
        }

        public async Task GetValues(int id)
        {
            Albums = await database.Albums.AsNoTracking()
                .Include(a => a.Artist)
                .Where(a => a.Artist.ID == id)
                .OrderBy(a => a.Title)
                .ToListAsync();

            Artist = await database.Artists.FirstOrDefaultAsync(a => a.ID == id);
            UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Liked = await database.LikedArtists
                .FirstOrDefaultAsync(a => a.User.Id == UserID && a.Artist.ID == id);
        }
    }
}
