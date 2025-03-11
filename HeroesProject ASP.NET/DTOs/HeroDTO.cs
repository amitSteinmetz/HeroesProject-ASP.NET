using HeroesProject_ASP.NET.Helpers;
using System.ComponentModel.DataAnnotations;

namespace HeroesProject_ASP.NET.DTOs
{
    public class HeroDTO
    {
        public string Name { get; set; }
        public string Ability { get; set; }
        public string[] SuitColors { get; set; }
        public double StartingPower { get; set; }
        public double? CurrentPower { get; set; }
        public string? ImgPath { get; set; }
    }
}
