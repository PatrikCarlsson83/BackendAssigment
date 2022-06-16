using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment3.Models
{
    public class Playlist
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Song> Songs { get; set; }
        [Required]
        public string UserID { get; set; }
        public IdentityUser User { get; set; }


    }
}
