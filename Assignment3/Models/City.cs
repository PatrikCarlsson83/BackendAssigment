using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Assignment3.Models
{
    public class City : IValidatableObject
    {
        public int ID { get; set; }
        [Required, MaxLength(255)]
        public string Name { get; set; }
        [JsonIgnore]
        [RegularExpression(@"^(\+|-)?(?:90(?:(?:\.0{1,6})?)|(?:[0-9]|[1-8][0-9])(?:(?:\.[0-9]{1,6})?))$",
            ErrorMessage = "Incorrect format: ##.######")]
        public double Latitude { get; set; }
        [JsonIgnore]
        [RegularExpression(@"^(\+|-)?(?:180(?:(?:\.0{1,6})?)|(?:[0-9]|[1-9][0-9]|1[0-7][0-9])(?:(?:\.[0-9]{1,6})?))$",
            ErrorMessage = "Incorrect format: ##.######")]
        public double Longitude { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrEmpty(Name))
            {
                yield return new ValidationResult(
                    "City must have a Name",
                    new[] { nameof(Name) }
                );
            }
        }
    }
}
