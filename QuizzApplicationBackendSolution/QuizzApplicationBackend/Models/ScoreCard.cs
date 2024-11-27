using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzApplicationBackend.Models
{
    public class ScoreCard
    {

        [Key]
        public int ScoreCardId { get; set; }

        public int QuizId { get; set; }

        public int UserId { get; set; }

        public int Score { get; set; }

        public double Acuuracy { get; set; }

        public Quiz Quiz { get; set; }

        public User User { get; set; }

        [NotMapped]
        public LeaderBoard LeaderBoard { get; set; }

    }
}
