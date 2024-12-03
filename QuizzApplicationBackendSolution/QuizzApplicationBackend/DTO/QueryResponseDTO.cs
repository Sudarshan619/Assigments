using System.ComponentModel.DataAnnotations;

namespace QuizzApplicationBackend.DTO
{
    public class QueryResponseDTO
    {
        public int QueryId { get; set; }

        public int ReportedBy { get; set; }

        public string QueryType { get; set; }

        public string Description { get; set; } = string.Empty;

        public bool IsResolved { get; set; } = false;
    }
}
