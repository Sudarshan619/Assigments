using QuizzApplicationBackend.Models;
using System.ComponentModel.DataAnnotations;

namespace QuizzApplicationBackend.DTO
{
    public class EditQuestionDTO
    {
        [Required(ErrorMessage = "Question Name is required.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Should be greter than 6 and less than 50 words")]
        public string QuestionName { get; set; }
        [Required(ErrorMessage = "Difficulty is required")]
        public Difficulties Difficulty { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public Categories Category { get; set; }

        [Required(ErrorMessage = "Points is required")]
        //[Range(1,ErrorMessage = "Should be positive integer")]

        public string SubCategory { get; set; }
        
        public int Points { get; set; }
    }
}
