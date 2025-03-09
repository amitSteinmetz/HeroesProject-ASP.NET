using HeroesProject_ASP.NET.Models;
using HeroesProject_ASP.NET.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var result = await _accountRepository.SignUp(signupModel);

            if (string.IsNullOrWhiteSpace(result)) return Unauthorized();

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            string result = await _accountRepository.Login(loginModel);

            if (string.IsNullOrWhiteSpace(result)) return Unauthorized();

            return Ok(result);
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
