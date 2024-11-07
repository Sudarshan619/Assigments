using Day29_BackendPlan.Misc;
using System.ComponentModel.DataAnnotations;

namespace Day29_BackendPlan.DTO
{
    public class ClaimReportDTO
    {

        [Required(ErrorMessage = "PolicyholderId cannot be empty")]
        public int PolicyHolderId{ get; set; }

        public string Description { get; set; }
        [Required(ErrorMessage = "DateTime cannot be empty")]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Policy holder cannot be empty")]
        public string SettlementForm { get; set; }
        public string DeathCertificate { get; set; }
        public string PolicyCertificate { get; set; }
        public string Photo { get; set; }
        public string AddressProof { get; set; }
        public string CancelledCheck { get; set; }
        public string? Others { get; set; }
    }
}
