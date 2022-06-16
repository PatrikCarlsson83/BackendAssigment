using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Assignment3.Data;
using Assignment3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment3.Pages.Playlists
{
    public class AddModel : PageModel
    {
        private readonly AppDbContext database;
        public AddModel(AppDbContext database)
        {
            this.database = database;
        }

        public Song Song { get; set; }
        public Playlist Playlist { get; set; }
        public List<Playlist> Playlists { get; set; }
        public string HttpUser { get; set; }
        public string Message { get; set; }

        public async Task OnGetAsync(int id)
        {
            Song = await database.Songs.FirstOrDefaultAsync(s => s.ID == id);
            HttpUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Playlists = await database.Playlists.Where(p => p.UserID == HttpUser).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(int id, int songId)
        {
            Song = await database.Songs.Include(a => a.Album)
                .Include(p => p.Playlists)
                .FirstOrDefaultAsync(s => s.ID == songId);
            var album = await database.Albums.FirstOrDefaultAsync(a => a.ID == Song.Album.ID);
            var playlist = await database.Playlists.FirstOrDefaultAsync(p => p.ID == id);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (playlist != null)
            {
                Song.Playlists.Add(playlist);
                await database.SaveChangesAsync();

                Message = Song.Title + " has been added to " + playlist.Name;
                await database.SaveChangesAsync();
            }
            else
            {
                Message = "No playlist was created.";
            }
            return RedirectToPage("/Albums/Index", new { id = album.ID, Message });
        }

        public async Task<IActionResult> OnPostCreateAsync(Playlist playlist, int id)
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
