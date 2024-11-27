using QuizzApplicationBackend.Models;

namespace QuizzApplicationBackend.DTO
{
    public class QuizQuestionReponseDTO
    {
        public int QuizId { get; set; }

        public string  Title { get; set; }

        public string  Difficulty { get; set; } 

        public int Duration { get; set; }
        public ICollection<QuestionResponseDTO> questions { get; set; }

        public int MaxPoints { get; set; }

    }
}
