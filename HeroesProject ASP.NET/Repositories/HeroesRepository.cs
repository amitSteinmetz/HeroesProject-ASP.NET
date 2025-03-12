using HeroesProject_ASP.NET.Data;
using HeroesProject_ASP.NET.Models;
using Microsoft.EntityFrameworkCore;

namespace HeroesProject_ASP.NET.Repositories
{
    public class HeroesRepository : IHeroesRepository
    {
        private readonly HeroesContext _context;

        public HeroesRepository(HeroesContext context)
        {
            _context = context;
        }

        public async Task<List<HeroModel>> GetAllHeroes()
        {
            var heroes = await _context.Heroes.Include(h => h.Trainer).ToListAsync();
            return heroes;
        }

        public async Task<HeroModel> GetHeroById(int id)
        {
            var hero = await _context.Heroes.Include(h => h.Trainer)
                .Where(h => h.Id == id)
                .SingleOrDefaultAsync();

            return hero;
        }

        public async Task<List<HeroModel>> GetAllAvailableHeroes()
        {
            var availableHeroes = await _context.Heroes.Include(h => h.Trainer).Where(h => h.Trainer == null).ToListAsync();
            return availableHeroes;
        }

        public async Task<int> CreateHero(NewHeroModel newHero)
        {
            HeroModel hero = new()
            {
                Name = newHero.Name,
                Ability = newHero.Ability,
                SuitColors = newHero.SuitColors,
                StartingPower = newHero.StartingPower,
                CurrentPower = newHero.StartingPower,
                DailyTrainingCount = 0,
                ImgPath = newHero.ImgPath
            };

            _context.Heroes.Add(hero);
            await _context.SaveChangesAsync();

            return hero.Id;
        }

        public async Task<int> DeleteHero(int heroId)
        {
            var hero = await _context.Heroes.FirstOrDefaultAsync(h => h.Id == heroId);

            if (hero != null && hero.Trainer == null) // hero with trainer cant be deleted here, only in TrainersRepository
            {
                _context.Heroes.Remove(hero);
                return await _context.SaveChangesAsync();
            }

            return -1;
        }

        public async Task<HeroModel> UpdateHero(int heroId, NewHeroModel updatedHero)
        {
            var hero = await _context.Heroes.Include(h => h.Trainer).FirstOrDefaultAsync(h => h.Id == heroId);

            if (hero != null)
            {
                hero.Name = updatedHero.Name;
                hero.Ability = updatedHero.Ability;
                hero.SuitColors = updatedHero.SuitColors;

                if (hero.Trainer == null)
                {
                    hero.StartingPower = updatedHero.StartingPower; // update startingPower only if has no trainer
                    hero.CurrentPower = updatedHero.StartingPower;
                }

                await _context.SaveChangesAsync();
            }

            return hero;
        }
    }
}
