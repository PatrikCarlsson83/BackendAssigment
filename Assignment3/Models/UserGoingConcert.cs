using Microsoft.AspNetCore.Identity;

namespace Assignment3.Models
{
    public class UserGoingConcert
    {
        public int ID { get; set; }
        public IdentityUser User { get; set; }
        public Concert Concert { get; set; }
    }
}
