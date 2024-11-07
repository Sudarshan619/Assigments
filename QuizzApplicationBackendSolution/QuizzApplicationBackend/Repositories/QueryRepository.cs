using Microsoft.EntityFrameworkCore;
using QuizzApplicationBackend.Context;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;

namespace QuizzApplicationBackend.Repositories
{
    public class QueryRepository : IRepository<int, Query>
    {
        private readonly QuizContext QueryContext;
        private readonly ILogger<QueryRepository> logger;

        public QueryRepository(QuizContext context, ILogger<QueryRepository> logger)
        {
            QueryContext = context;
            this.logger = logger;
        }

        public async Task<Query> Add(Query entity)
        {
            try
            {
                QueryContext.Queries.Add(entity);
                await QueryContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Not able to add Query");
            }
        }

        public async Task<Query> Delete(int id)
        {
            try
            {
                var res = await Get(id);
                QueryContext.Queries.Remove(res);
                await QueryContext.SaveChangesAsync();
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception("Not able to delete Question");
            }
        }

        public async Task<Query> Get(int id)
        {
            try
            {
                var policy = await QueryContext.Queries.FirstOrDefaultAsync(e => e.QueryId == id);
                await QueryContext.SaveChangesAsync();
                if (policy != null)
                {
                    return policy;
                }
                throw new NotFoundException("Query not found");

            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task<IEnumerable<Query>> GetAll()
        {
            try
            {
                var policy = await QueryContext.Queries.ToListAsync();
                await QueryContext.SaveChangesAsync();
                if (policy != null)
                {
                    return policy;
                }
                throw new NotFoundException("Collection empty");

            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public Task<Query> Update(int id, Query entity)
        {
            throw new NotImplementedException();
        }
    }
}
