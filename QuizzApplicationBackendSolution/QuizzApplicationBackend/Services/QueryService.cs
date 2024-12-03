using AutoMapper;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;

namespace QuizzApplicationBackend.Services
{
    public class QueryService : IQueryService
    {
        private readonly IRepository<int, Query> _repository;
        private readonly IMapper _mapper;

        public QueryService(IRepository<int,Query> repository,IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<bool> CreateQuery(QueryDTO question)
        {
            try
            {
                var query = _mapper.Map<Query>(question);
                await _repository.Add(query);
                return query!=null;
            }
            catch (Exception ex) {
                throw new Exception("Could not generate query");          
            }
        }

        public async Task<bool> DeleteQuery(int id)
        {
            try
            {
                var res = await _repository.Delete(id);
                return res != null;
            }
            catch (NotFoundException)
            {
                throw new NotFoundException($"Query with ID {id} not found.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error while deleting query: " + ex.Message);
            }
        }

        public async Task<bool> EditQuery(int id, QueryDTO question)
        {
            try
            {
                var existingQuery = await _repository.Get(id);
                var updatedQuestion = _mapper.Map(question, existingQuery);

                var result = await _repository.Update(id, updatedQuestion);
                return result != null;
            }
            catch (NotFoundException)
            {
                throw new NotFoundException($"Query with ID {id} not found.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error while editing query: " + ex.Message);
            }
        }

        public async Task<QueryResponseDTO> GetQuery(int id)
        {
            try
            {
                var query = await _repository.Get(id);
                return _mapper.Map<QueryResponseDTO>(query);
            }
            catch (NotFoundException)
            {
                throw new NotFoundException($"Query with ID {id} not found.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving query: " + ex.Message);
            }
        }

        public async Task<IEnumerable<QueryResponseDTO>> GetAllQuery()
        {
            try
            {
                var query = await _repository.GetAll();
                return _mapper.Map<IEnumerable<QueryResponseDTO>>(query);
            }
            catch (NotFoundException)
            {
                throw new NotFoundException($"Queries not found.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving query: " + ex.Message);
            }
        }
    }
}
