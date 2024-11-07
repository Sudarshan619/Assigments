using Day27_Webapi_EF.Misc;
using Day27_Webapi_EF.Models;
using System.ComponentModel.DataAnnotations;

namespace Day27_Webapi_EF.DTO
{
    public class UserCreateDTO
    {
        [UsernameValidator]
        public string Username { get; set; } = string.Empty;
  
        [RegularExpression(@"^[a-zA-Z0-9]*$",
            ErrorMessage = "Password pattern worng")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Role cannot be empty")]
        public Roles Role { get; set; }
    }
   
}
