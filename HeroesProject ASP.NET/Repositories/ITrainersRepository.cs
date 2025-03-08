using HeroesProject_ASP.NET.Models;

namespace HeroesProject_ASP.NET.Repositories
{
    public interface ITrainersRepository
    {
        Task<int> AddHero(int trainerId, int heroId);
        Task<List<HeroModel>> GetTrainerHeroes(int trainerId);
        Task<int> DeleteHero(int trainerId, int heroId);
        Task<HeroModel> UpdateHeroCurrentPower(int trainerId, int heroId, double updatedCurrentPower);
        Task<List<TrainerModel>> GetAllTrainers();
        Task<TrainerModel> GetTrainer(int trainerId);
    }
}