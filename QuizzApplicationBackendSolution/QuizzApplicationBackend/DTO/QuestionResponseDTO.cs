using QuizzApplicationBackend.Models;

namespace QuizzApplicationBackend.DTO
{
    public class QuestionResponseDTO
    {
        public string QuestionName { get; set; }
        public Categories Category { get; set; }
        public ICollection<OptionDTO> Options { get; set; }
    }
}
