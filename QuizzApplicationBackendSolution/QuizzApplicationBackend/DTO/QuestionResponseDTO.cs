using QuizzApplicationBackend.Models;

namespace QuizzApplicationBackend.DTO
{
    public class QuestionResponseDTO
    {
        public int QuestionId { get; set; }
        public string QuestionName { get; set; }
        public Categories Category { get; set; }

        public int Points { get; set; }
        public ICollection<OptionResponseDTO> Options { get; set; }
    }
}
