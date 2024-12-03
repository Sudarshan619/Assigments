using System.ComponentModel.DataAnnotations;

namespace QuizzApplicationBackend.DTO
{
    public class QueryDTO
    {

        [Required(ErrorMessage = "ReportedBy is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "ReportedBy must be a positive number.")]
        public int ReportedBy { get; set; }

        [Required(ErrorMessage = "QueryType is required.")]
        [StringLength(50, ErrorMessage = "QueryType cannot exceed 50 characters.")]
        public string QueryType { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; } = string.Empty;

        public bool IsResolved { get; set; } = false;
    }
}
