using HeroesProject_ASP.NET.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeroesProject_ASP.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class TrainersController : ControllerBase
    {
        private readonly ITrainersRepository _trainersRepository;

        public TrainersController(ITrainersRepository trainersRepository)
        {
            _trainersRepository = trainersRepository;
        }

        [HttpPut("add-hero/{heroId}")]
        public async Task<IActionResult> addHeroToTrainer(int heroId)
        {
            var userName = User?.Identity?.Name;
            if (userName == null) return BadRequest();

            var result = await _trainersRepository.AddHeroToTrainer(userName, heroId);

            if (result == -1) return BadRequest();

            return Ok();
        }

        [HttpGet("my-heroes")]
        public async Task<IActionResult> GetTrainerHeroes()
        {
            var userName = User?.Identity?.Name;
            if (userName == null) return BadRequest();

            var trainerHeroes = await _trainersRepository.GetTrainerHeroes(userName);

            if (trainerHeroes == null) return NotFound();

            return Ok(trainerHeroes);
        }

        [HttpDelete("{heroId}")]
        public async Task<IActionResult> DeleteHeroFromTrainer(int heroId)
        {
            var userName = User?.Identity?.Name;
            if (userName == null) return BadRequest();

            var result = await _trainersRepository.DeleteHeroFromTrainer(userName, heroId);

            if (result == -1) return BadRequest();

            return Ok();
        }

        [HttpPatch("train/{heroId}")]
        public async Task<IActionResult> TrainHero(int heroId)
        {
            var userName = User?.Identity?.Name;
            if (userName == null) return BadRequest();

            var heroCurrentPower = await _trainersRepository.TrainHero(userName, heroId);

            if (heroCurrentPower == null) return BadRequest();

            return Ok(heroCurrentPower);
        }
    }
}
