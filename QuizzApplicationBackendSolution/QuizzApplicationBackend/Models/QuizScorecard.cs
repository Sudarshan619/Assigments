

using System.ComponentModel.DataAnnotations;

namespace QuizzApplicationBackend.Models
{
    public class QuizScorecard
    {
        [Key]
        public int QuizScoreCardNo { get; set; }

        public int QuizId { get; set; }

        public int ScoreCardId{ get; set; }
       
        public IEnumerable<ScoreCard> ScoreCards { get; set; }

        public IEnumerable<Quiz> Quizes { get; set; }
    }
}
