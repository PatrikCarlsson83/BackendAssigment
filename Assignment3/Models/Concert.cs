using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3.Models
{
    public class Concert
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime Date { get; set; }
        [Required]
        public City City { get; set; }
        public List<Artist> Artists { get; set; }

    }
}
