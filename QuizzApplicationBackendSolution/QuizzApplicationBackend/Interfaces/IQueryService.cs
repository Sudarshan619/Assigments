using QuizzApplicationBackend.DTO;

namespace QuizzApplicationBackend.Interfaces
{
    public interface IQueryService
    {
        public Task<bool> CreateQuery(QueryDTO question);

        public Task<bool> DeleteQuery(int id);

        public Task<bool> EditQuery(int id, QueryDTO question);

        public Task<QueryDTO> GetQuery(int id);

    }
}
