using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment3.Models
{
    public class Album
    {
        public int ID { get; set; }
        [Required, MaxLength(255)]
        public string Title { get; set; }
        [Required, MaxLength(255)]
        public Artist Artist { get; set; }
        public List<Song> Songs { get; set; }
    }
}
