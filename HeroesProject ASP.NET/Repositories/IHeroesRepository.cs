using HeroesProject_ASP.NET.Models;

namespace HeroesProject_ASP.NET.Repositories
{
    public interface IHeroesRepository
    {
        Task<int> CreateHero(NewHeroModel newHero);
        Task<List<HeroModel>> GetAllHeroes();
        Task<HeroModel> GetHeroById(int id);
        Task<List<HeroModel>> GetAllAvailableHeroes();
        Task<int> DeleteHero(int heroId);
        Task<HeroModel> UpdateHero(int heroId, NewHeroModel updatedHero);
    }
}