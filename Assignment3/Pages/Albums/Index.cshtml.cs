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

namespace Assignment3.Pages.Albums
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext database;
        private readonly UserManager<IdentityUser> _userManager;
        public IndexModel(AppDbContext database, UserManager<IdentityUser> userManager)
        {
            this.database = database;
            _userManager = userManager;
        }

        public Album Album { get; set; }
        public Song Song { get; set; }
        public List<Song> Songs { get; set; }
        public List<Playlist> Playlists { get; set; }
        public string HttpUser { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Message { get; set; }

        public async Task OnGetAsync(int id)
        {
            Album = await database.Albums.FirstOrDefaultAsync(a => a.ID == id);
            Songs = await database.Songs.Where(a => a.Album.ID == id).ToListAsync();
        }

        public async Task<IActionResult> OnPostAddAsync(Song song, int id)
        {
            Album = await database.Albums.FirstOrDefaultAsync(a => a.ID == id);

            if (song.Title != null)
            {
                Song newSong = new Song();
                newSong.Title = song.Title;
                newSong.Album = Album;
                database.Songs.Add(newSong);
            }

            await database.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            HttpUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Playlists = await database.Playlists.Where(p => p.UserID == HttpUser).ToListAsync();

            if (Playlists.Any())
            {
                return RedirectToPage("/Playlists/Add", new { id });
            }
            else
            {
                return RedirectToPage("/Playlists/Create", new { id });
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            Album = await database.Albums.FirstOrDefaultAsync(a => a.ID == id);

            Message = Album.Title + " has been deleted";

            database.Albums.Remove(Album);
            await database.SaveChangesAsync();

            return RedirectToPage("/Artists/Index", new { Message });
        }
    }
}
