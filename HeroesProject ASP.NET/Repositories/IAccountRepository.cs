using HeroesProject_ASP.NET.Models;
using Microsoft.AspNetCore.Identity;

namespace HeroesProject_ASP.NET.Repositories
{
    public interface IAccountRepository
    {
        Task<string> SignUp(SignupModel signupModel);
        Task<string> Login(LoginModel loginModel);
        Task<int> DeleteUser(Guid id);
    }
}