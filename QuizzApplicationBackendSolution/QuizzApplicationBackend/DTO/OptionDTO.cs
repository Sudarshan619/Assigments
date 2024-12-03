using System.ComponentModel.DataAnnotations;

namespace QuizzApplicationBackend.DTO
{
    public class OptionDTO
    {
        [Required(ErrorMessage = "QuestionId is required")]
        public int QuestionId { get; set; }

        [Required(ErrorMessage = "Option value is required")]
        public string Text { get; set; }

        [Required(ErrorMessage = "IsCorrect is required")]
        public Boolean IsCorrect { get; set; }
    }
}
