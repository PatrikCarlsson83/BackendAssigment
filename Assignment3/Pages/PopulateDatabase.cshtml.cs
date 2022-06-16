using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment3.Data;
using Assignment3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment3.Pages
{
    public class PopulateDatabaseModel : PageModel
    {
        private readonly AppDbContext database;
        public PopulateDatabaseModel(AppDbContext database)
        {
            this.database = database;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            await AddMusicCatalog();
            await AddCitiesCatalog();
            await AddConcertsCatalog();
            return RedirectToPage("/Index");
        }

        public async Task AddMusicCatalog()
        {
            var artists = new List<string>
            {
                "Converge",
                "Idles",
                "Dillinger Escape Plan",
                "King Krule",
                "That Handsome Devil",
                "Defeater",
                "Di Leva",
                "Backstreet Boys",
                "Kendrick Lamar"
            };

            foreach (var artist in artists)
            {
                await AddArtistAsync(artist);
            };

            var convergeAlbums = new List<string>
            {
                "Jane Doe",
                "The Dusk In Us"
            };

            var IdlesAlbums = new List<string>
            {
                "Ultra Mono",
                "Brutalism"
            };

            var DillingerAlbums = new List<string>
            {
                "Miss Machine",
                "Irony Is A Dead Scene"
            };

            var KingKruleAlbums = new List<string>
            {
                "Man Alive!",
                "The OOZ"
            };

            var handsomeDevilAlbums = new List<string>
            {
                "History Is A Suicide Note",
                "That Handsome Devil"
            };

            foreach (var album in convergeAlbums)
            {
                await AddAlbumAsync("Converge", album);
            }

            foreach (var album in IdlesAlbums)
            {
                await AddAlbumAsync("Idles", album);
            }

            foreach (var album in DillingerAlbums)
            {
                await AddAlbumAsync("Dillinger Escape Plan", album);
            }

            foreach (var album in KingKruleAlbums)
            {
                await AddAlbumAsync("King Krule", album);
            }

            foreach (var album in handsomeDevilAlbums)
            {
                await AddAlbumAsync("That Handsome Devil", album);
            }

            var janeDoeSongs = new List<string>
            {
                "Concubine",
                "Fault and Fracture",
                "Distance and Meaning",
                "Hell to Pay",
                "Homewrecker",
                "The Broken Vow",
                "Bitter and Then Some",
                "Heaven in Her Arms",
                "Phoenix in Flight",
                "Phoenix in Flames",
                "Thaw",
                "Jane Doe"
            };

            var duskInUsSongs = new List<string>
            {
                "A Single Tear",
                "Eye of the Quarrel",
                "Under Duress",
                "Arkhipov Calm",
                "I Can tell You About Pain",
                "The Dusk in Us",
                "Wildlife",
                "Murk & Marrow",
                "Trigger",
                "Broken By Light",
                "Cannibals",
                "Thousands of Miles Between Us",
                "Reptilian"
            };

            var ultraMonoSongs = new List<string>
            {
                "War",
                "Grounds",
                "Mr. Motivator",
                "Anxiety",
                "Kill Them With Kindness",
                "Model Village",
                "Ne Touche Pas Moi",
                "Carcinogenic",
                "Reigns",
                "To Lover",
                "A Hymn",
                "Danke"
            };

            var brutalismSongs = new List<string>
            {
                "Heel / Heal",
                "Well Done",
                "Mother",
                "Date Night",
                "Faith in the City",
                "1049 Gotho",
                "Divide and Conquer",
                "Rachel Khoo",
                "Stendahl Syndrome",
                "Exeter",
                "Benzocaine",
                "White Privilege",
                "Slow Savage"
            };

            var missMachineSongs = new List<string>
            {
                "Panasonic Youth",
                "Sunshine the Werewolf",
                "Highway Robbery",
                "Van Damsel",
                "Phone Home",
                "We Are the Storm",
                "Crutch Field Tongs",
                "Setting Fire to Sleeping Giants",
                "Baby's First Coffin",
                "Unretorfied",
                "The Perfect Design"
            };

            var ironyIsADeadSceneSongs = new List<string>
            {
                "Hollywood Squares",
                "Pig Latin",
                "When Good Dogs DO Bad Things",
                "Come To Daddy"
            };

            var theOozSongs = new List<string>
            {
                "Biscuit Town",
                "THe Locomotive",
                "Dum Surfer",
                "Slush Puppy",
                "Bermondsey Bosom (left)",
                "Logos",
                "Sublunary",
                "Lonely Blue",
                "Cadet Limbo",
                "Emergency Blimp",
                "Czech One",
                "A Slide In (New Drugs)",
                "Vidual",
                "Bermondsey Bosom (Right)",
                "Half Man Half Shark",
                "The Cadet Leaps",
                "The Ooz",
                "Midnight 01 (Deep Sea Diver)",
                "La Lune"
            };

            var manAliveSongs = new List<string>
            {
                "Cellular",
                "Supermarché",
                "Stoned Again",
                "Comet Face",
                "The Dream",
                "Perfecto Miserable",
                "Alone, Omen 3",
                "Slinky",
                "Airport Antenatal Airplane",
                "(Don't Let The Dragon) Draag On",
                "Theme for the Cross",
                "Underclass",
                "Energy Fleets",
                "Please Complete Thee"
            };

            var historyIsASuicideNoteSongs = new List<string>
            {
                "Savages",
                "Fake",
                "Emergency",
                "Echo Chamber",
                "Pendulumonium"
            };

            var thatHandsomeDevilSongs = new List<string>
            {
                "Standing Room In Heaven",
                "Yada Yada",
                "Sleep It Off",
                "Elephant Bones",
                "Dating Tips",
                "Miss America",
                "James Dean"
            };

            foreach (var song in janeDoeSongs)
            {
                await AddSongsAsync("Jane Doe", song);
            }

            foreach (var song in duskInUsSongs)
            {
                await AddSongsAsync("The Dusk In Us", song);
            }

            foreach (var song in ultraMonoSongs)
            {
                await AddSongsAsync("Ultra Mono", song);
            }

            foreach (var song in brutalismSongs)
            {
                await AddSongsAsync("Brutalism", song);
            }

            foreach (var song in missMachineSongs)
            {
                await AddSongsAsync("Miss Machine", song);
            }

            foreach (var song in ironyIsADeadSceneSongs)
            {
                await AddSongsAsync("Irony Is A Dead Scene", song);
            }

            foreach (var song in theOozSongs)
            {
                await AddSongsAsync("The OOz", song);
            }

            foreach (var song in manAliveSongs)
            {
                await AddSongsAsync("Man Alive!", song);
            }

            foreach (var song in historyIsASuicideNoteSongs)
            {
                await AddSongsAsync("History Is A Suicide Note", song);
            }

            foreach (var song in thatHandsomeDevilSongs)
            {
                await AddSongsAsync("That Handsome Devil", song);
            }
        }

        public async Task AddCitiesCatalog()
        {
            await AddCity("Stockholm", 59.328168, 18.059637);
            await AddCity("Skarpnäck", 59.265234, 18.142417);
            await AddCity("Umeå", 63.827967, 20.262216);
            await AddCity("Göteborg", 57.721223, 11.973463);
            await AddCity("Malmö", 55.611607, 13.001546);
            await AddCity("Oxelösund", 58.679289, 17.074766);
            await AddCity("Nyköping", 58.756620, 17.010568);
            await AddCity("Norrköping", 58.591273, 16.193191);
            await AddCity("Jönköping", 57.786926, 14.162886);
            await AddCity("Katrineholm", 58.998715, 16.204810);
        }

        

        public async Task AddArtistAsync(string name)
        {
            var artist = new Artist();
            artist.Name = name;

            await database.Artists.AddAsync(artist);
            await database.SaveChangesAsync();
        }

        public async Task AddAlbumAsync(string artist, string title)
        {
            var findArtist = await database.Artists.Include(a => a.Albums).FirstOrDefaultAsync(a => a.Name == artist);
            var album = new Album();
            album.Title = title;
            album.Artist = findArtist;

            await database.Albums.AddAsync(album);
            await database.SaveChangesAsync();
        }

        public async Task AddSongsAsync(string album, string title)
        {
            var findAlbum = await database.Albums.FirstOrDefaultAsync(a => a.Title == album);
            Song song = new Song();
            song.Title = title;
            song.Album = findAlbum;
            database.Songs.Add(song);

            await database.SaveChangesAsync();

        }

        public async Task AddCity(string name, double lat, double lon)
        {
            var city = new City();
            city.Name = name;
            city.Latitude = lat;
            city.Longitude = lon;

            await database.Cities.AddAsync(city);
            await database.SaveChangesAsync();
        }

        public async Task AddConcertsCatalog()
        {
            await AddConcert("Stockholm", "Warp Tour", "2022-06-12", 
                    new List<string> { "Idles", "Dillinger Escape Plan", "King Krule" });
            await AddConcert("Skarpnäck", "Local Fest", "2022-08-01",
                    new List<string> { "Di Leva", "Converge"});
            await AddConcert("Umeå", "Bring Your Mask Tour!", "2022-07-24",
                    new List<string> { "Defeater", "Backstreet Boys", "That Handsome Devil" });
            await AddConcert("Göteborg", "Stop the Pandemic", "2023-03-24",
                    new List<string> { "King Krule", "Kendrick Lamar", "Di leva" });
            await AddConcert("Malmö", "Vaccinated Welcome Tour!", "2023-05-01",
                    new List<string> { "Di Leva", "Kendrick Lamar", "Backstreet Boys" });
            await AddConcert("Nyköping", "Metal Tour of the Year", "2024-04-03",
                    new List<string> { "Dillinger Escape Plan", "Idles", "That Handsome Devil" });
            await AddConcert("Oxelösund", "Metal Tour of the Year", "2024-04-05",
                    new List<string> { "Dillinger Escape Plan", "Idles", "That Handsome Devil" });
            await AddConcert("Katrineholm", "Metal Tour of the Year", "2024-04-07",
                    new List<string> { "Dillinger Escape Plan", "Idles", "King Krule" });
            await AddConcert("Jönköping", "Stay at Home", "2022-05-17",
                    new List<string> { "Di Leva", "Kendrick Lamar", "Backstreet Boys" });
            await AddConcert("Norrköping", "Stay at Home", "2022-05-19",
                    new List<string> { "Di Leva", "Kendrick Lamar", "Backstreet Boys" });
            await AddConcert("Oxelösund", "Sommar vid Kusten", "2022-07-09",
                    new List<string> { "Di Leva", "That Handsome Devil", "Dillinger Escape Plan" });
            await AddConcert("Nyköping", "Sommar vid Kusten", "2022-07-11",
                    new List<string> { "Di Leva", "That Handsome Devil", "Dillinger Escape Plan" });

        }
        public async Task AddConcert(string city, string name, string date, List<string> artists)
        {
            var findCity = await database.Cities.FirstOrDefaultAsync(c => c.Name == city);
            var concert = new Concert();
            concert.Name = name;
            concert.City = findCity;
            concert.Date = Convert.ToDateTime(date);

            await database.Concerts.AddAsync(concert);
            await database.SaveChangesAsync();

            var concertArtist = await database.Concerts.FirstOrDefaultAsync(c => c.ID == concert.ID);

            foreach (var artist in artists)
            {
                var foundArtist = await database.Artists.Include(c => c.Concerts)
                    .FirstOrDefaultAsync(a => a.Name == artist);
                foundArtist.Concerts.Add(concertArtist);
            }

            await database.SaveChangesAsync();
        }
    }
}
