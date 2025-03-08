using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HeroesProject_ASP.NET.Models
{
    public class HeroModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Ability {  get; set; }
       
        public string? StartTrainingDate { get; set; }
        public string[] SuitColors { get; set; }
        
        public double StartingPower { get; set; }
        
        public double? CurrentPower { get; set; }

        public TrainerModel? Trainer { get; set; }
    }
}
