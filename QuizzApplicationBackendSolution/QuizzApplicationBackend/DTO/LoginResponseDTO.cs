using QuizzApplicationBackend.Models;

namespace QuizzApplicationBackend.DTO
{
    public class LoginResponseDTO
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;

        public string Image { get; set; }
        public Roles Role { get; set; }
        public string Token { get; set; } = string.Empty;
        public int StatusCode { get; set; }
    }
}
