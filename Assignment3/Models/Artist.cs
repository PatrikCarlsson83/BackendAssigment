using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Assignment3.Models
{
    public class Artist
    {
        public int ID { get; set; }
        [Required, MaxLength(255)]
        public string Name { get; set; }
        public List<Album> Albums { get; set; }
        [JsonIgnore]
        public List<Concert> Concerts { get; set; }
    }
}
