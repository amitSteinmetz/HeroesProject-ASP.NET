﻿using HeroesProject_ASP.NET.Data;
using HeroesProject_ASP.NET.DTOs;
using HeroesProject_ASP.NET.Helpers;
using HeroesProject_ASP.NET.Models;
using HeroesProject_ASP.NET.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeroesProject_ASP.NET.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
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

            List<HeroDTO> heroesDTO = new();
            foreach (var hero in heroes)
            {
                heroesDTO.Add(HeroUtilities.constructHeroDTO(hero));
            }

            return Ok(heroesDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHeroById(int id)
        {
            var hero = await _heroesRepository.GetHeroById(id);

            if (hero == null) return NotFound();

            return Ok(hero);
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAllAvailableHeroes()
        {
            var availableHeroes = await _heroesRepository.GetAllAvailableHeroes();

            if (availableHeroes.Count == 0) return NotFound();

            List<HeroDTO> availableHeroesDTO = new();
            foreach (var hero in availableHeroes)
            {
                availableHeroesDTO.Add(HeroUtilities.constructHeroDTO(hero));
            }

            return Ok(availableHeroesDTO);
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateHero([FromBody] NewHeroModel hero)
        {
            var heroId = await _heroesRepository.CreateHero(hero);
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
