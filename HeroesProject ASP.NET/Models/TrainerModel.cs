using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HeroesProject_ASP.NET.Models
{
    public class TrainerModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public IList<HeroModel>? Heroes { get; set; }
        public AppUser User { get; set; }
    }
}
