using HeroesProject_ASP.NET.Data;
using HeroesProject_ASP.NET.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HeroesProject_ASP.NET.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly HeroesContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountRepository(HeroesContext context , UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> SignUp(SignupModel signupModel)
        {
            AppUser user = new()
            {
                FirstName = signupModel.FirstName,
                LastName = signupModel.LastName,
                Email = signupModel.Email,
                UserName = signupModel.Email
            };

            var result = await _userManager.CreateAsync(user, signupModel.Password);

            if (result.Succeeded)
            {
                var trainer = new TrainerModel
                {
                    Name = signupModel.FirstName + " " + signupModel.LastName,
                    User = user
                };

                await _context.Trainers.AddAsync(trainer);
                await _context.SaveChangesAsync();
            }

            return result;
        }

        public async Task<string> Login(LoginModel loginModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password,
                false, false);
            if (!result.Succeeded) return null;

            string token = CreateToken(loginModel.Email);
            return token;
        }

        private string CreateToken(string email)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<AppUser> UpdateUser(AppUser updatedUser)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == updatedUser.Id);

            if (user != null)
            {
                user.FirstName = updatedUser.FirstName;
                user.LastName = updatedUser.LastName;
                user.UserName = updatedUser.UserName;

                var trainer = await _context.Trainers.FindAsync(updatedUser.Id);
                if (trainer != null) trainer.Name = user.FirstName + " " + user.LastName;

                await _context.SaveChangesAsync();
            }

            return user;
        }

        public async Task<int> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id.ToString());

            if (user != null)
            {
                var trainer = await _context.Trainers.Include(t => t.Heroes).FirstOrDefaultAsync(t => t.User.Id == id.ToString());
                if (trainer != null)
                {
                    if (trainer.Heroes != null)
                    {
                        foreach (var hero in trainer.Heroes)
                        {
                            hero.StartTrainingDate = null;
                        }
    
                        trainer.Heroes.Clear();
                    }
                    _context.Trainers.Remove(trainer);
                }
                
                _context.Users.Remove(user);

                return await _context.SaveChangesAsync();
            }

            return -1;
        }
    }
}
