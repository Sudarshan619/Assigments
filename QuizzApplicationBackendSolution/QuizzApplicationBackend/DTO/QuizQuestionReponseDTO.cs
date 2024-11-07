using QuizzApplicationBackend.Models;

namespace QuizzApplicationBackend.DTO
{
    public class QuizQuestionReponseDTO
    {
        public ICollection<QuestionResponseDTO> questions { get; set; }

        public int MaxPoints { get; set; }

    }
}
