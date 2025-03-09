using HeroesProject_ASP.NET.Models;

namespace HeroesProject_ASP.NET.Repositories
{
    public interface ITrainersRepository
    {
        Task<int> AddHeroToTrainer(string userName, int heroId);
        Task<List<HeroModel>> GetTrainerHeroes(string userName);
        Task<int> DeleteHeroFromTrainer(string userName, int heroId);
        Task<double?> TrainHero(string userName, int heroId);
    }
}