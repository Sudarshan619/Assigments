using System.ComponentModel.DataAnnotations;

namespace QuizzApplicationBackend.Models
{
    public class LeaderBoard
    {
        [Key]
        public int LeaderBoardId { get; set; }

        public string LeaderBoardName { get; set; } = string.Empty;

        public Categories Categories {  get; set; }

        public IEnumerable<ScoreCard> ScoreCard { get; set; }
    }
}
