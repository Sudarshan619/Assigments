namespace Day27_Webapi_EF.DTO
{
    public class LoginResponseDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public int StatusCode { get; set; } 
    }
}
