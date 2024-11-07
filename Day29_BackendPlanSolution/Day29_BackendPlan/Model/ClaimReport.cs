using System.ComponentModel.DataAnnotations;

namespace Day29_BackendPlan.Model
{
    public class ClaimReport
    {
        [Key]
        public int ReportId { get; set; }
        public int PolicyHolderId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }

        public string SettlementForm {  get; set; }

        public string DeathCertificate { get; set; }

        public string PolicyCertificate { get; set; }

        public string Photo {  get; set; }

        public string AddressProof { get; set; }

        public string CancelledCheck { get; set; }

        public string? Others { get; set; }

        public PolicyHolder PolicyHolder { get; set; }

       
    }
}
