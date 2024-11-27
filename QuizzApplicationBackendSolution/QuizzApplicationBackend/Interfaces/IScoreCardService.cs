using QuizzApplicationBackend.DTO;

namespace QuizzApplicationBackend.Interfaces
{
    public interface IScoreCardService
    {
        Task<bool> CreateScoreCard(SubmittedOptionDTO submittedOptionDTO);
        Task<bool> UpdateScoreCard(int id, ScoreCardDTO scoreCardDto);

        Task<double> CalculateAccuracy(SubmittedOptionDTO submittedOptionDTO, int score);
        Task<bool> DeleteScoreCard(int id);
        Task<ScoreCardResponseDTO> GetScoreCard(int id);

        Task<IEnumerable<ScoreCardResponseDTO>> GetAllScoreCards();
    }
}
