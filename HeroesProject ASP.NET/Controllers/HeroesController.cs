using HeroesProject_ASP.NET.Data;
using HeroesProject_ASP.NET.Models;
using HeroesProject_ASP.NET.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeroesProject_ASP.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        // Should it contain all avialbe heroes or all heroes? if we delete - we need to nkow if it's only from avaible and not conntected to trainers,
        // or from all heroes and in this case we shoukd delete the hero on traintr list too.
        // Add hero to all heroes
        // delete hero
        // update hero details
        // get all heroes
        // get all available heroes
        // get hero by id ? 

        private readonly IHeroesRepository _heroesRepository;

        public HeroesController(IHeroesRepository heroesRepository)
        {
            _heroesRepository = heroesRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllHeroes()
        {
            var heroes = await _heroesRepository.GetAllHeroes();
            if (heroes == null) return NotFound();

            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHeroById(int id)
        {
            var hero = await _heroesRepository.GetHeroById(id);

            if (hero == null) return NotFound();

            return Ok(hero);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddHero([FromBody] NewHeroModel hero)
        {
            var heroId = await _heroesRepository.AddHero(hero);
            return CreatedAtAction(nameof(GetHeroById), new { id = heroId }, hero);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHero(int id)
        {
            int result = await _heroesRepository.DeleteHero(id);

            if (result == -1) return BadRequest();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHero([FromRoute] int id , [FromBody] NewHeroModel hero)
        {
            var result = await _heroesRepository.UpdateHero(id, hero);

            if (result == null) return BadRequest();

            return Ok(result);
        }
    }
}
