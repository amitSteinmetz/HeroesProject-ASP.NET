using HeroesProject_ASP.NET.Models;
using HeroesProject_ASP.NET.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HeroesProject_ASP.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainersController : ControllerBase
    {
        private readonly ITrainersRepository _trainersRepository;

        public TrainersController(ITrainersRepository trainersRepository)
        {
            _trainersRepository = trainersRepository;
        }

        [HttpPut("{trainerId}")]
        public async Task<IActionResult> addHero(int trainerId , [FromBody] int heroId)
        {
            var result = await _trainersRepository.AddHero(trainerId, heroId);

            if (result == -1) return BadRequest();

            return Ok();
        }

        [HttpGet("{trainerId}/heroes")]
        public async Task<IActionResult> GetTrainerHeroes(int trainerId)
        {
            var trainerHeroes = await _trainersRepository.GetTrainerHeroes(trainerId);

            if (trainerHeroes == null) return NotFound();

            return Ok(trainerHeroes);
        }

        [HttpDelete("{trainerId}")]
        public async Task<IActionResult> DeleteHero(int trainerId, [FromBody] int heroId)
        {
            var result = await _trainersRepository.DeleteHero(trainerId, heroId);

            if (result == -1) return BadRequest();

            return Ok();
        }

        [HttpPatch("{trainerId}/{heroId}")]
        public async Task<IActionResult> UpdateHeroCurrentPower(int trainerId, int heroId, [FromBody] double updatedCurrentPower)
        {
            var hero = await _trainersRepository.UpdateHeroCurrentPower(trainerId, heroId, updatedCurrentPower);

            if (hero == null) return BadRequest();

            return Ok(hero);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllTrainers()
        {
            var trainers = await _trainersRepository.GetAllTrainers();

            if (trainers.Count == 0) return NotFound();

            return Ok(trainers);
        }

        [HttpGet("{trainerId}")]
        public async Task<IActionResult> GetTrainer(int trainerId)
        {
            var trainer = await _trainersRepository.GetTrainer(trainerId);

            if (trainer == null) return NotFound();

            return Ok(trainer);
        }
    }
}
