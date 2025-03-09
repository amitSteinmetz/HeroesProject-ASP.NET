using Microsoft.AspNetCore.Identity;

namespace HeroesProject_ASP.NET.Models
{
    public class TrainerModel : IdentityUser
    {
        public string Name { get; set; }
        public List<HeroModel>? Heroes { get; set; }
    }
}