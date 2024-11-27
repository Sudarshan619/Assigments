using QuizzApplicationBackend.Models;

namespace QuizzApplicationBackend.DTO
{
    public class QuestionDTO
    {
        public string QuestionName { get; set; }

        public Difficulties Difficulty { get; set; }
        public Categories Category { get; set; }
        public int Points { get; set; }
    }
}
