using QuizzApplicationBackend.Models;

namespace QuizzApplicationBackend.DTO
{
    public class QuizDTO
    {
        public int CreatorId { get; set; }

        public Categories Category { get; set; }

        public string Title { get; set; }

        public Difficulties Difficulty { get; set; }

        public int NoOfQuestions {  get; set; }
    }
}
