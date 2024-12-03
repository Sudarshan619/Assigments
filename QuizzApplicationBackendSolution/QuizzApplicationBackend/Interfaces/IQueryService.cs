using QuizzApplicationBackend.DTO;

namespace QuizzApplicationBackend.Interfaces
{
    public interface IQueryService
    {
        public Task<bool> CreateQuery(QueryDTO question);

        public Task<bool> DeleteQuery(int id);

        public Task<bool> EditQuery(int id, QueryDTO question);

        public Task<QueryResponseDTO> GetQuery(int id);

        public Task<IEnumerable<QueryResponseDTO>> GetAllQuery();

    }
}
