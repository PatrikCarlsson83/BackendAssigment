using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Assignment3.Models
{
    public class LikedArtist
    {
        public int ID { get; set; }
        [Required]
        public IdentityUser User { get; set; }
        [Required]
        public Artist Artist { get; set; }

    }
}
