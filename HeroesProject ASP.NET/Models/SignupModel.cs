using System.ComponentModel.DataAnnotations;

namespace HeroesProject_ASP.NET.Models
{
    public class SignupModel
    {
        [Required]
        public string FirstName { get; set; }
         
        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*\W).{8,}$",
            ErrorMessage = 
            "Password must be at least 8 characters long, include at least one uppercase letter," +
            " one lowercase letter, one number, and one special character.")]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}

