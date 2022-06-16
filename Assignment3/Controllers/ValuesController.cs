using Assignment3.Data;
using Assignment3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment3.Controllers
{
    [Route("api/musicdive")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly AppDbContext database;
        public ValuesController(AppDbContext database)
        {
            this.database = database;
        }


        // GET: /api/musicdive
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Concert>>> GetArtistAsync()
        {
            var concerts = await database.Concerts.AsNoTracking()
               .Include(a => a.Artists)
               .Include(c => c.City)
               .OrderBy(c => c.Date)
               .ToListAsync();

            return concerts;
        }

        //GET: /api/city
        [AllowAnonymous]
        [HttpGet("/api/city")]
        public async Task<ActionResult<IEnumerable<City>>> GetCitiesAsync()
        {
            var cities = await database.Cities.AsNoTracking()
                .OrderBy(c => c.Name)
                .ToListAsync();

            return cities;
        }

        //GET: api/city/concert
        [AllowAnonymous]
        [HttpGet("/api/concert/city")]
        public async Task<ActionResult<IEnumerable<Concert>>> GetConcertByCityAsync([FromQuery] string city)
        {
            var concerts = new List<Concert>();
            if (city == "Everywhere")
            {
                concerts = await database.Concerts.AsNoTracking()
                .Include(c => c.City)
                .Include(a => a.Artists)
                .OrderBy(c => c.Date)
                .ToListAsync();
            }
            else
            {
                concerts = await database.Concerts.AsNoTracking()
                .Include(c => c.City)
                .Include(a => a.Artists)
                .Where(c => c.City.Name == city)
                .OrderBy(c => c.Date)
                .ToListAsync();
            }

            return concerts;
        }

        //GET: /api/city/artist
        [AllowAnonymous]
        [HttpGet("/api/concert/artist")]
        public async Task<ActionResult<IEnumerable<Concert>>> GetCitiesArtistAsync([FromQuery] string artist, string city)
        {
            var concertList = new List<Concert>();
            var concerts = new List<Concert>();

            if (city != "Everywhere")
            {
                concertList = await database.Concerts.AsNoTracking()
                .Include(c => c.City)
                .Include(a => a.Artists)
                .Where(c => c.City.Name == city)
                .OrderBy(c => c.Date)
                .ToListAsync();

                foreach (var concert in concertList)
                {
                    foreach (var item in concert.Artists)
                    {
                        if (item.Name.ToLower() == artist.ToLower())
                        {
                            concerts.Add(concert);
                        }
                    }
                }
                return concerts;
            }
            else
            {
                concertList = await database.Concerts.AsNoTracking()
                .Include(a => a.Artists)
                .Include(c => c.City)
                .OrderBy(c => c.Date)
                .ToListAsync();

                foreach (var concert in concertList)
                {
                    foreach (var item in concert.Artists)
                    {
                        if (item.Name.ToLower() == artist.ToLower())
                        {
                            concerts.Add(concert);
                        }
                    }
                }
                return concerts;
            }
        }
    }
}
