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
    public class CreateModel : PageModel
    {
        private readonly AppDbContext database;
        public CreateModel(AppDbContext database)
        {
            this.database = database;
        }
        public Playlist Playlist { get; set; }
        public string HttpUser { get; set; }
        public string Message { get; set; }

        public async Task<IActionResult> OnPostAsync(Playlist playlist, int id)
        {
            HttpUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var song = await database.Songs.Include(a => a.Album)
                .ThenInclude(a => a.Artist)
                .Include(p => p.Playlists)
                .FirstOrDefaultAsync(s => s.ID == id);
            var album = await database.Albums.FirstOrDefaultAsync(a => a.ID == song.Album.ID);

            if (playlist.Name != null)
            {
                var newPlaylist = new Playlist();
                newPlaylist.Name = playlist.Name;
                newPlaylist.UserID = HttpUser;

                await database.Playlists.AddAsync(newPlaylist);
                await database.SaveChangesAsync();

                var playlistAdd = await database.Playlists.FirstOrDefaultAsync(p => p.Name == playlist.Name);
                song.Playlists.Add(playlistAdd);
                await database.SaveChangesAsync();

                Message = song.Title + " has been added to " + playlist.Name;
                await database.SaveChangesAsync();
            }
            else
            {
                Message = "No playlist was created.";
            }

            return RedirectToPage("/Albums/Index", new { id = album.ID, Message });
        }
    }
}