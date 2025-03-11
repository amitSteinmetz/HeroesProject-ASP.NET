using HeroesProject_ASP.NET.DTOs;
using HeroesProject_ASP.NET.Models;
using HeroesProject_ASP.NET.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace HeroesProject_ASP.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public UserAccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupModel signupModel)
        {
            var user = await _accountRepository.SignUp(signupModel);

            if (user == null) return Unauthorized();

            var userDTO = new TrainerDTO
            {
                UserName = user.UserName
            };

            return Ok(userDTO);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var loggedUserRes = await _accountRepository.Login(loginModel);

            if (loggedUserRes == null) return Unauthorized();

            return Ok(loggedUserRes);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var result = await _accountRepository.DeleteUser(id);

            if (result == -1) return BadRequest();

            return Ok();
        }
    }
}
