using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizzApplicationBackend.Models
{
    public class LeaderBoard
    {
        [Key]
        public int LeaderBoardId { get; set; }

        public string LeaderBoardName { get; set; } = string.Empty;

        public int QuizId { get; set; }

        public DateTime StartingFrom { get; set; } = DateTime.Now;

        public DateTime Ending { get; set; } = DateTime.Now.AddDays(7);

        public Categories Categories {  get; set; }

        //public ICollection<ScoreCard> ScoreCard { get; set; } = new List<ScoreCard>();

        public Quiz Quiz { get; set; }

    }
}
