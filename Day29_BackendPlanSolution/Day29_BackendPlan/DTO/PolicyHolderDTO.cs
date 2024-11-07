using System.ComponentModel.DataAnnotations;

namespace Day29_BackendPlan.DTO
{
    public class PolicyHolderDTO
    {
        [Required(ErrorMessage ="name is required")]
        [RegularExpression(@"^[a-zA-Z0-9]*$",
            ErrorMessage = "Password pattern worng")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone is required")]
        public long Phone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;
    }
}
