using HeroesProject_ASP.NET.DTOs;
using HeroesProject_ASP.NET.Models;
using Microsoft.AspNetCore.Identity;

namespace HeroesProject_ASP.NET.Repositories
{
    public interface IAccountRepository
    {
        Task<TrainerModel> SignUp(SignupModel signupModel);
        Task<LoggedUserDTO> Login(LoginModel loginModel);
        Task<int> DeleteUser(Guid id);
    }
}