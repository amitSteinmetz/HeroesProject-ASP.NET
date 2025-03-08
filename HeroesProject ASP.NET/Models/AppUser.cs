using Microsoft.AspNetCore.Identity;

namespace HeroesProject_ASP.NET.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
