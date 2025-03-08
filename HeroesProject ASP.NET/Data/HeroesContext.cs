using HeroesProject_ASP.NET.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HeroesProject_ASP.NET.Data
{
    public class HeroesContext : IdentityDbContext<AppUser>
    {
        public HeroesContext(DbContextOptions<HeroesContext> options) : base(options) { }
        
        public DbSet<HeroModel> Heroes { get; set; }

        public DbSet<TrainerModel> Trainers { get; set; }
    }
}
