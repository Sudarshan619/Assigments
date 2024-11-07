namespace Day27_Webapi_EF.DTO
{
    public class ErrorResponseDTO
    {
        public int ErrorNumber { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
