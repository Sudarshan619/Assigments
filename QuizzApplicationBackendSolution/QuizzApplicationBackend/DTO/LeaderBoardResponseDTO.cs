using QuizzApplicationBackend.Models;

namespace QuizzApplicationBackend.DTO
{
    public class LeaderBoardResponseDTO
    {
        public int LeaderBoardId { get; set; }
        public string LeaderBoardName { get; set; }= string.Empty;

        public DateTime StartingFrom { get; set; }

        public DateTime Ending { get; set; }

        public string Category { get; set; } 
        public IEnumerable<ScoreCardResponseDTO> ScoreCards { get; set; }
    }
}
