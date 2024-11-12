using QuizzApplicationBackend.Models;

namespace QuizzApplicationBackend.DTO
{
    public class LeaderBoardResponseDTO
    {
        public string LeaderBoardName { get; set; }= string.Empty;

        public string Category { get; set; } 
        public IEnumerable<ScoreCardResponseDTO> ScoreCards { get; set; }
    }
}
