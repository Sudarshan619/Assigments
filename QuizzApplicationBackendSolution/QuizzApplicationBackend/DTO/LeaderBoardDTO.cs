using System.ComponentModel.DataAnnotations;
using QuizzApplicationBackend.Models;

namespace QuizzApplicationBackend.DTO
{
    public class LeaderBoardDTO
    {
        [Required(ErrorMessage = "LeaderBoard Name is required.")]
        [StringLength(100, ErrorMessage = "LeaderBoard Name cannot exceed 100 characters.")]
        public string LeaderBoardName { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public Categories Category { get; set; }

        [Required(ErrorMessage = "Quiz ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quiz ID must be a positive integer.")]
        public int QuizId { get; set; }
    }
}
