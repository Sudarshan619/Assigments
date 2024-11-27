using QuizzApplicationBackend.Models;
using System.ComponentModel.DataAnnotations;

namespace QuizzApplicationBackend.DTO
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(50,MinimumLength =6, ErrorMessage = "Username should be greater than 6 and less than 50 characters.")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Role is required.")]
        public Roles Role { get; set; }
    }
}
