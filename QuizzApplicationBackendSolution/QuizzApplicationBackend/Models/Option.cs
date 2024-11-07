using System.ComponentModel.DataAnnotations;

namespace QuizzApplicationBackend.Models
{
    public class Option
    {
        [Key]
        public int OptionId { get; set; }

        public int QuestionId { get; set; }

        public string Text { get; set; }

        public Boolean IsCorrect { get; set; }

        public Question Question { get; set; }
    }
}
