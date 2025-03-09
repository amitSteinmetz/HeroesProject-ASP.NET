using HeroesProject_ASP.NET.Models;

namespace HeroesProject_ASP.NET.Helpers
{
    public class TrainHeroUtilities
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
    }
}
