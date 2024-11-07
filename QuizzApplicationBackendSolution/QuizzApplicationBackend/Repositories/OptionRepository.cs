using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuizzApplicationBackend.Context;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;
using TestProject1;

namespace QuizzApplicationBackend.Repositories
{
    public class OptionRepository : IRepository<int, Option>
    {
        private readonly QuizContext OptionContext;
        private readonly ILogger<OptionRepository> logger;

        public OptionRepository(QuizContext context, ILogger<OptionRepository> logger)
        {
            OptionContext = context;
            this.logger = logger;
        }

        public async Task<Option> Add(Option entity)
        {
            try
            {
                OptionContext.Options.Add(entity);
                await OptionContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding Option.");
                throw new CouldNotAddException( ex.Message);
            }
        }

        public async Task<Option> Delete(int id)
        {
            try
            {
                var option = await Get(id); // Calls Get method, which will throw NotFoundException if not found
                OptionContext.Options.Remove(option);
                await OptionContext.SaveChangesAsync();
                return option;
            }
            catch (NotFoundException ex)
            {
                logger.LogError(ex, $"Option with ID {id} not found for deletion.");
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting Option.");
                throw new Exception("Unable to delete Option", ex);
            }
        }

        public async Task<Option> Get(int id)
        {
            var option = await OptionContext.Options.FirstOrDefaultAsync(e => e.OptionId == id);
            if (option == null)
            {
                logger.LogWarning($"Option with ID {id} not found.");
                throw new NotFoundException("Option not found");
            }
            return option;
        }

        public async Task<IEnumerable<Option>> GetAll()
        {
            var options = await OptionContext.Options.ToListAsync();
            if (options.Count == 0)
            {
                logger.LogWarning("No Options found in the collection.");
                throw new CollectionEmptyException("Collection empty");
            }
            return options;
        }

        public Task<Option> Update(int id, Option entity)
        {
            throw new NotImplementedException();
        }
    }
}
