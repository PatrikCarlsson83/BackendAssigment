using Assignment3.Data;
using Assignment3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Assignment3.Pages
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

        [BindProperty(SupportsGet = true)]
        public string Message { get; set; }
        public List<LikedArtist> Artists { get; set; }
        public List<Playlist> Playlists { get; set; }
        public List<UserGoingConcert> Concerts { get; set; }
        public bool DatabaseAny { get; set; }
        public async Task OnGetAsync()
        {
            if (!database.Artists.Any())
            {
                DatabaseAny = true;
            }

            var httpUser = await _userManager.GetUserAsync(HttpContext.User);
            Artists = await database.LikedArtists.AsNoTracking()
                .Include(a => a.Artist)
                .Where(u => u.User == httpUser)
                .OrderBy(a => a.Artist.Name)
                .ToListAsync();

            Playlists = await database.Playlists.AsNoTracking()
                .Where(p => p.User == httpUser)
                .OrderBy(p => p.Name)
                .ToListAsync();

            Concerts = await database.UserGoingConcerts.AsNoTracking()
                .Include(c => c.Concert)
                .Where(p => p.User == httpUser)
                .OrderBy(c => c.Concert.Name)
                .ToListAsync();

        }
    }
}
