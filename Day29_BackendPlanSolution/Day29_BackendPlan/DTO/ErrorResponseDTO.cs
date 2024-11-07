using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Day29_BackendPlan.DTO
{
    internal class ErrorResponseDTO
    {
        public string ErrorMessage { get; set; }
        public int ErrorNumber { get; set; }
    }
}