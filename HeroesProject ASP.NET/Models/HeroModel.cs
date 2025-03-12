using HeroesProject_ASP.NET.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HeroesProject_ASP.NET.Models
{
    public class HeroModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public HeroAbility Ability {  get; set; }
       
        public DateTime? StartTrainingDate { get; set; }
        public string[] SuitColors { get; set; }
        
        public double StartingPower { get; set; }
        
        public double? CurrentPower { get; set; }

        public TrainerModel? Trainer { get; set; }
        public DateTime? LastTrainingDate { get; set; }
        public int DailyTrainingCount { get; set; }
        public string? ImgPath { get; set; }
    }
}
