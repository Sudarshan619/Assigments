using System.ComponentModel.DataAnnotations;

namespace Day29_BackendPlan.DTO
{
    public class PolicyItemDTO
    {
        [Required(ErrorMessage = "Policyid is required")]
        public int PolicyId { get; set; }

        [Required(ErrorMessage = "PolicyHolderid is required")]
        public int PolicyHolderId { get; set; } 
    }
}
