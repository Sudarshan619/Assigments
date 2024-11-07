using System.ComponentModel.DataAnnotations;
using Day29_BackendPlan.Misc;

namespace Day29_BackendPlan.DTO
{
    public class PolicyDTO
    {
        [FileValidation]
        public string PolicyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Policy type is required")]
        public string PolicyType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Policy value is required")]
        public double PolicyValue { get; set; }
    }
}
