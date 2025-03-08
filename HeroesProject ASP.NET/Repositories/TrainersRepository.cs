using HeroesProject_ASP.NET.Data;
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

        public async Task<List<HeroModel>> GetTrainerHeroes(int trainerId)
        {
            var trainer = await _context.Trainers.Include(t => t.Heroes).FirstOrDefaultAsync(t => t.Id == trainerId);

            return trainer?.Heroes.ToList();
        }

        public async Task<int> AddHero(int trainerId, int heroId)
        {
            var trainer = await _context.Trainers.Include(t => t.Heroes).FirstOrDefaultAsync(t => t.Id == trainerId);
            var hero = await _context.Heroes.Include(h => h.Trainer).FirstOrDefaultAsync(h => h.Id == heroId);

            if (trainer != null && hero != null)
            {
                if (hero.Trainer != null) return -1; // hero is already taken

                hero.StartTrainingDate = DateTime.Today.ToString();

                if (trainer.Heroes == null) trainer.Heroes = new List<HeroModel>() { hero }; // first hero - create new list
                else trainer.Heroes.Add(hero);
           
                return await _context.SaveChangesAsync();
            }

            return -1;
        }

        public async Task<int> DeleteHero(int trainerId, int heroId)
        {
            var trainer = await _context.Trainers.Include(t => t.Heroes).FirstOrDefaultAsync(t => t.Id == trainerId);
            var hero = await _context.Heroes.Include(h => h.Trainer).FirstOrDefaultAsync(h => h.Id == heroId);

            if (trainer != null && hero != null)
                if (trainer.Heroes != null && trainer.Heroes.Contains(hero))
                {
                    trainer.Heroes.Remove(hero);
                    return await _context.SaveChangesAsync();
                }

            return -1;
        }

        public async Task<HeroModel> UpdateHeroCurrentPower(int trainerId, int heroId, double updatedCurrentPower)
        {
            var trainer = await _context.Trainers.Include(t => t.Heroes).FirstOrDefaultAsync(t => t.Id == trainerId);
            var hero = await _context.Heroes.Include(h => h.Trainer).FirstOrDefaultAsync(h => h.Id == heroId);

            if (trainer != null && hero != null)
                if (trainer.Heroes != null && trainer.Heroes.Contains(hero))
                {
                    hero.CurrentPower = updatedCurrentPower;
                    await _context.SaveChangesAsync();
                    return hero;
                }

            return null;
        }

        public async Task<List<TrainerModel>> GetAllTrainers()
        {
            return await _context.Trainers.Include(t => t.Heroes).ToListAsync();
        }
        
        public async Task<TrainerModel> GetTrainer(int trainerId)
        {
            var trainer = await _context.Trainers.Include(t => t.Heroes).FirstOrDefaultAsync(t => t.Id == trainerId);
            return trainer;
        }
    }
}
