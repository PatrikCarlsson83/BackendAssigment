using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment3.Models
{
    public class Song
    {
        public int ID { get; set; }
        [Required, MaxLength(255)]
        public string Title { get; set; }
        [Required]
        public Album Album { get; set; }
        public List<Playlist> Playlists { get; set; }
    }
}
