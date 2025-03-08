using HeroesProject_ASP.NET.Models;

namespace HeroesProject_ASP.NET.Repositories
{
    public interface IHeroesRepository
    {
        Task<int> AddHero(NewHeroModel newHero);
        Task<List<HeroModel>> GetAllHeroes();
        Task<HeroModel> GetHeroById(int id);
        Task<int> DeleteHero(int heroId);
        Task<HeroModel> UpdateHero(int heroId, NewHeroModel updatedHero);
    }
}