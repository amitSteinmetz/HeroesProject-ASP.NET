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

                trainer.Heroes = trainer.Heroes ?? new List<HeroModel>(){};
                trainer.Heroes.Add(hero);
        
                return await _context.SaveChangesAsync();
            }

            return -1;
        }

        public async Task<int> DeleteHeroFromTrainer(string userName, int heroId)
        {
            var trainer = await _context.Users.Include(t => t.Heroes).FirstOrDefaultAsync(t => t.Email == userName);
            var hero = await _context.Heroes.Include(h => h.Trainer).FirstOrDefaultAsync(h => h.Id == heroId);

            if (trainer == null || hero == null || trainer.Heroes == null || !trainer.Heroes.Contains(hero))
            {
                return -1;
            }

            HeroUtilities.resetHero(hero);
            trainer.Heroes.Remove(hero);

            return await _context.SaveChangesAsync();
        }

        public async Task<double?> TrainHero(string userName, int heroId)
        {
            var trainer = await _context.Users.Include(t => t.Heroes).FirstOrDefaultAsync(t => t.Email == userName);
            var hero = await _context.Heroes.Include(h => h.Trainer).FirstOrDefaultAsync(h => h.Id == heroId);

            if (trainer == null
                || hero == null
                || trainer.Heroes == null
                || !trainer.Heroes.Contains(hero)
                || hero.DailyTrainingCount >= 5)
            {
                return null;
            }

            var heroUpdatedPower = HeroUtilities.TrainHeroHandler(hero);
            await _context.SaveChangesAsync();
            return heroUpdatedPower;
        }
    }
}
