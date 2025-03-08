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

        [HttpPost("")]
        public async Task<IActionResult> Signup([FromBody] SignupModel signupModel)
        {
            var result = await _accountRepository.SignUp(signupModel);

            if (result.Succeeded) return Ok(result.Succeeded);
            
            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            string result = await _accountRepository.Login(loginModel);

            if (string.IsNullOrWhiteSpace(result)) return Unauthorized();

            return Ok(result);
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateUser([FromBody] AppUser updatedUser)
        {
            var result = await _accountRepository.UpdateUser(updatedUser);

            if (result == null) return BadRequest();

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
