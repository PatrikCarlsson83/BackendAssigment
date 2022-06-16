using System.Security.Claims;
using System.Threading.Tasks;
using Assignment3.Data;
using Assignment3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment3.Pages.Playlists
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext database;

        public DeleteModel(AppDbContext database)
        {
            this.database = database;
        }
        public Playlist Playlist { get; set; }
        public Song Song { get; set; }
        public string Message { get; set; }
        public async Task<IActionResult> OnGetAsync(int songId, int id)
        {
            Song = await database.Songs.FirstOrDefaultAsync(s => s.ID == songId);
            Playlist = await database.Playlists.FirstOrDefaultAsync(p => p.ID == id);

            string UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (UserID != Playlist.UserID)
            {
                return Forbid();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int songId, int id)
        {
            Song = await database.Songs.FirstOrDefaultAsync(s => s.ID == songId);
            Playlist = await database.Playlists.Include(s => s.Songs).FirstOrDefaultAsync(p => p.ID == id);

            string UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (UserID != Playlist.UserID)
            {
                return Forbid();
            }

            Message = Song.Title + " removed from " + Playlist.Name;
            Playlist.Songs.Remove(Song);
            await database.SaveChangesAsync();

            return RedirectToPage("./Index", new { id, Message });
        }
    }
}
