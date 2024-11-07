namespace QuizzApplicationBackend.DTO
{
    public class QueryDTO
    {
        public int ReportedBy { get; set; }

        public string QueryType { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}
