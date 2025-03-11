using HeroesProject_ASP.NET.DTOs;
using HeroesProject_ASP.NET.Models;

namespace HeroesProject_ASP.NET.Helpers
{
    public class HeroUtilities
    {
        public static void TrainHeroHandler(HeroModel hero, bool sameDay)
        {
            hero.CurrentPower = TrainHeroResult(hero.CurrentPower);
            hero.LastTrainingDate = DateTime.Today;
            hero.DailyTrainingCount = sameDay ? hero.DailyTrainingCount + 1 : 1;
        }

        private static double? TrainHeroResult(double? currentPower)
        {
            if (currentPower == null) return null;

            Random random = new();
            int rand = random.Next(100, 110);

            double updatedPower = (double)currentPower * rand / 100;
            return Math.Round(updatedPower, 2);
        }

        public static HeroDTO constructHeroDTO(HeroModel hero)
        {
            return new HeroDTO
            {
                Id = hero.Id,
                Name = hero.Name,
                Ability = hero.Ability.ToString(),
                SuitColors = hero.SuitColors,
                StartingPower = hero.StartingPower,
                CurrentPower = hero.CurrentPower,
                ImgPath = hero.ImgPath
            };
        }
    }
}
