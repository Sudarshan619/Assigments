using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Models;

namespace QuizzApplicationBackend.Interfaces
{
    public enum Choice
    {
        Score,
        Username

    }
    public interface ILeaderBoardService
    {
        public Task<bool> CreateLeaderBoard(LeaderBoardDTO leaderBoard);

        public Task<IEnumerable<LeaderBoardResponseDTO>> GetAllLeaderBoard(int pageNumber,int pageSize);

        public Task<LeaderBoardResponseDTO> GetLeaderBoard(int id);

        public Task<LeaderBoardResponseDTO> SortLeaderBoard(Choice choice, int id);

        public Task<bool> UpdateLeaderBoard(int id, LeaderBoardDTO leaderBoard);

        public Task<bool> DeleteLeaderBoard(int id);
    }
}
