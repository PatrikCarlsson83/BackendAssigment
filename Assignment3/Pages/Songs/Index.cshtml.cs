using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment3.Data;
using Assignment3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment3.Pages.Songs
{
    public class IndexModel : PageModel
    {

        private readonly AppDbContext database;
        public IndexModel(AppDbContext database)
        {
            this.database = database;
        }

        public Song Song { get; set; }
        public string Message { get; set; }
        public async Task OnGetAsync(int id)
        {
            Song = await database.Songs.FirstOrDefaultAsync(s => s.ID == id);
        }

        public async Task<IActionResult> OnPostEdit(Song song, int id)
        {
            Song = await database.Songs.Include(a => a.Album).FirstOrDefaultAsync(s => s.ID == id);
            var album = await database.Albums.FirstOrDefaultAsync(a => a.ID == Song.Album.ID);

            Message = Song.Title + " has been changed to " + song.Title;

            Song.Title = song.Title;
            await database.SaveChangesAsync();

            return RedirectToPage("/Albums/Index", new { album.ID, Message });
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            Song = await database.Songs.Include(a => a.Album).FirstOrDefaultAsync(s => s.ID == id);
            var album = await database.Albums.FirstOrDefaultAsync(a => a.ID == Song.Album.ID);

            Message = Song.Title + " has been deleted";

            database.Songs.Remove(Song);
            await database.SaveChangesAsync();
            return RedirectToPage("/Albums/Index", new { album.ID, Message });

        }
    }
}
