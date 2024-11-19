using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzApplicationBackend.Models
{
    public class Query
    {
        [Key]
        public int QueryId { get; set; }
 
        public int ReportedBy { get; set; }

        public string QueryType { get; set; }

        public string Description { get; set; } = string.Empty;

        public Boolean IsResolved   { get; set; } = false;

        public User User { get; set; }


    }
}
