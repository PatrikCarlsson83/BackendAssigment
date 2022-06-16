using System.Security.Claims;
using System.Threading.Tasks;
using Assignment3.Data;
using Assignment3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment3.Pages.Playlists
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext database;
        public IndexModel(AppDbContext database)
        {
            this.database = database;
        }

        public Playlist Playlist { get; set; }
        public Song Song { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Message { get; set; }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Playlist = await database.Playlists.Include(s => s.Songs)
                       .FirstOrDefaultAsync(p => p.ID == id);

            string UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (UserID != Playlist.UserID)
            {
                return Forbid();
            }

            return Page();
        }

    public async Task<IActionResult> OnPostEditAsync(Playlist playlist, int id)
        {
            Playlist = await database.Playlists.Include(s => s.Songs)
                       .FirstOrDefaultAsync(p => p.ID == id);

            string UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (UserID != Playlist.UserID)
            {
                return Forbid();
            }

            Message = Playlist.Name + " has been changed to " + playlist.Name;

            Playlist.Name = playlist.Name;

            await database.SaveChangesAsync();
            return RedirectToPage("./Index", new { id, Message });
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            Playlist = await database.Playlists.Include(s => s.Songs)
                       .FirstOrDefaultAsync(p => p.ID == id);

            string UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (UserID != Playlist.UserID)
            {
                return Forbid();
            }

            Message = Playlist.Name + " has been deleted.";

            database.Playlists.Remove(Playlist);
            await database.SaveChangesAsync();

            return RedirectToPage("../Index", new { Message });
        }
    }
}
