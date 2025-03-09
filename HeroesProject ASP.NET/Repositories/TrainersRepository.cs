using HeroesProject_ASP.NET.Data;
using HeroesProject_ASP.NET.Helpers;
using HeroesProject_ASP.NET.Models;
using Microsoft.EntityFrameworkCore;

namespace HeroesProject_ASP.NET.Repositories
{
    public class TrainersRepository : ITrainersRepository
    {
        private readonly HeroesContext _context;

        public TrainersRepository(HeroesContext context)
        {
            _context = context;
        }
        public async Task<List<HeroModel>> GetTrainerHeroes(string userName)
        {
            var trainer = await _context.Users.Include(t => t.Heroes).FirstOrDefaultAsync(t => t.Email == userName);
            return trainer?.Heroes?.ToList();  
        }

        public async Task<int> AddHeroToTrainer(string userName, int heroId)
        {
            var trainer = await _context.Users.Include(t => t.Heroes).FirstOrDefaultAsync(t => t.Email == userName);
            var hero = await _context.Heroes.Include(h => h.Trainer).FirstOrDefaultAsync(h => h.Id == heroId);

            if (trainer != null && hero != null)
            {
                if (hero.Trainer != null) return -1; // hero is already taken

                hero.StartTrainingDate = DateTime.Today;

                if (trainer.Heroes == null) trainer.Heroes = new List<HeroModel>() { hero }; // first hero - create new list
                else trainer.Heroes.Add(hero);

                return await _context.SaveChangesAsync();
            }

            return -1;
        }

        public async Task<int> DeleteHeroFromTrainer(string userName, int heroId)
        {
            var trainer = await _context.Users.Include(t => t.Heroes).FirstOrDefaultAsync(t => t.Email == userName);
            var hero = await _context.Heroes.Include(h => h.Trainer).FirstOrDefaultAsync(h => h.Id == heroId);

            if (trainer != null && hero != null)
                if (trainer.Heroes != null && trainer.Heroes.Contains(hero))
                {
                    hero.StartTrainingDate = null;
                    hero.LastTrainingDate = null;
                    hero.DailyTrainingCount = null;
                    hero.CurrentPower = hero.StartingPower;

                    trainer.Heroes.Remove(hero);

                    return await _context.SaveChangesAsync();
                }

            return -1;
        }

        public async Task<double?> TrainHero(string userName, int heroId)
        {
            var trainer = await _context.Users.Include(t => t.Heroes).FirstOrDefaultAsync(t => t.Email == userName);
            var hero = await _context.Heroes.Include(h => h.Trainer).FirstOrDefaultAsync(h => h.Id == heroId);

            if (trainer != null && hero != null)
                if (trainer.Heroes != null && trainer.Heroes.Contains(hero))
                {
                    if (!hero.LastTrainingDate.HasValue || (hero.LastTrainingDate.HasValue && hero.LastTrainingDate.Value < DateTime.Today))
                        TrainHeroUtilities.TrainHeroHandler(hero, false);
                    else if (hero.DailyTrainingCount < 5)
                        TrainHeroUtilities.TrainHeroHandler(hero, true);

                    var changes = await _context.SaveChangesAsync();

                    return (changes != 0) ? hero.CurrentPower : null; // if no changes - hero was traind 5 times today, that should lead to bad request
                }

            return null;
        }
    }
}
