using HeroesProject_ASP.NET.Models;
using Microsoft.AspNetCore.Identity;

namespace HeroesProject_ASP.NET.Repositories
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUp(SignupModel signupModel);
        Task<string> Login(LoginModel loginModel);
        Task<AppUser> UpdateUser(AppUser updatedUser);
        Task<int> DeleteUser(Guid id);
    }
}