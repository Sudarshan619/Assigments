using QuizzApplicationBackend.Models;

namespace QuizzApplicationBackend.DTO
{
    public class QuestionParameterDTO
    {
        public Categories category { get; set; }

        public int NoOfQuestions { get; set; }
    }
}
