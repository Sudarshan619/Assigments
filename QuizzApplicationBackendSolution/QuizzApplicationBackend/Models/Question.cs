using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzApplicationBackend.Models
{
   
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        public string QuestionName { get; set; } = string.Empty;

        public Categories Category { get; set; }
        public string SubCategory { get; set; } = string.Empty ;

        public int Points { get; set; }

        public Difficulties Difficulty { get; set; }

        public ICollection<Option> Options { get; set; } = new List<Option>();


    }
}
