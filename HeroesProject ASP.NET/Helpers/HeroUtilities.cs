using HeroesProject_ASP.NET.DTOs;
using HeroesProject_ASP.NET.Models;

namespace HeroesProject_ASP.NET.Helpers
{
    public class HeroUtilities
    {
        public static void TrainHeroHandler(HeroModel hero)
        {
            hero.CurrentPower = TrainHeroResult(hero.CurrentPower);
            bool sameDay = (hero.LastTrainingDate != null) && (hero.LastTrainingDate.Value == DateTime.Today);
            hero.DailyTrainingCount = sameDay ? hero.DailyTrainingCount + 1 : 1;
            hero.LastTrainingDate = DateTime.Today;
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

        public static void resetHero(HeroModel hero)
        {
            hero.StartTrainingDate = null;
            hero.LastTrainingDate = null;
            hero.DailyTrainingCount = 0;
            hero.CurrentPower = hero.StartingPower;
        }
    }
}
