using QuizzApplicationBackend.DTO;

namespace QuizzApplicationBackend.Interfaces
{
    public interface IScoreCardService
    {
        Task<bool> CreateScoreCard(ScoreCardDTO scoreCardDto);
        Task<bool> UpdateScoreCard(int id, ScoreCardDTO scoreCardDto);
        Task<bool> DeleteScoreCard(int id);
        Task<ScoreCardResponseDTO> GetScoreCard(int id);
    }
}
