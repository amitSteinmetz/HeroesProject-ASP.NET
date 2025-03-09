using HeroesProject_ASP.NET.Helpers;
using System.ComponentModel.DataAnnotations;

namespace HeroesProject_ASP.NET.Models
{
    public class NewHeroModel
    {
        [Required(ErrorMessage = "Please add a name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please add an ability")]
        public HeroAbility Ability { get; set; }

        [Required(ErrorMessage = "Please add suit colors")]
        public string[] SuitColors { get; set; }

        [Required(ErrorMessage = "Please add hero starting power")]
        public double StartingPower { get; set; }
    }
}
