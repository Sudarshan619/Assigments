namespace QuizzApplicationBackend.DTO
{
    public class LoginResponseDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public int StatusCode { get; set; }
    }
}
