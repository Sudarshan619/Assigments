using QuizzApplicationBackend.Models;

namespace QuizzApplicationBackend.DTO
{
    public class CreateUserDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string Email { get; set; }

        public Roles Role {get; set;}

    }
}
