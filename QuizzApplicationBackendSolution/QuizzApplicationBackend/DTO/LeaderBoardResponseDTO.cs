using QuizzApplicationBackend.Models;

namespace QuizzApplicationBackend.DTO
{
    public class LeaderBoardResponseDTO
    {
        public string LeaderBoardName { get; set; }= string.Empty;

        public Categories Category { get; set; } = 0;
        public IEnumerable<ScoreCardResponseDTO> ScoreCards { get; set; }
    }
}
