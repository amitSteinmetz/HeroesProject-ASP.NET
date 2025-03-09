using HeroesProject_ASP.NET.Data;
using HeroesProject_ASP.NET.Helpers;
using HeroesProject_ASP.NET.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HeroesProject_ASP.NET.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly HeroesContext _context;
        private readonly UserManager<TrainerModel> _userManager;
        private readonly SignInManager<TrainerModel> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountRepository(HeroesContext context , UserManager<TrainerModel> userManager, SignInManager<TrainerModel> signInManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<string> SignUp(SignupModel signupModel)
        {
            TrainerModel trainer = new()
            {
                Name = signupModel.Name,
                Email = signupModel.Email,
                UserName = signupModel.Email
            };

            var result = await _userManager.CreateAsync(trainer, signupModel.Password);

            if (result.Succeeded) return trainer.Id;
            return null;
        }

        public async Task<string> Login(LoginModel loginModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password,
                false, false);
            if (!result.Succeeded) return null;

            var user = await _userManager.FindByEmailAsync(loginModel.Email);

            return TokenUtilities.CreateToken(_configuration, user);
        }

        public async Task<int> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id.ToString());

            if (user != null)
            {
                if (user.Heroes != null)
                {
                    foreach (var hero in user.Heroes)
                    {
                        hero.StartTrainingDate = null;
                    }
                    user.Heroes.Clear();
                }

                _context.Users.Remove(user);
                return await _context.SaveChangesAsync();
            }

            return -1;
        }
    }
}
