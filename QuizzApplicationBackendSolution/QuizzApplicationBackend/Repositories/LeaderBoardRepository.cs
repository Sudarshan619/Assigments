using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using QuizzApplicationBackend.Context;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;


namespace QuizzApplicationBackend.Repositories
{
    public class LeaderBoardRepository : IRepository<int, LeaderBoard>
    {
        private readonly QuizContext LeaderBoardContext;
        private readonly ILogger<LeaderBoardRepository> logger;

        public LeaderBoardRepository(QuizContext context, ILogger<LeaderBoardRepository> logger)
        {
            LeaderBoardContext = context;
            this.logger = logger;
        }

        public async Task<LeaderBoard> Add(LeaderBoard entity)
        {
            try
            {
                LeaderBoardContext.LeaderBoards.Add(entity);
                await LeaderBoardContext.SaveChangesAsync();
                if(entity == null)
                {
                    throw new CouldNotAddException("could not add leader board");
                }
                return entity;
            }
            catch (CouldNotAddException ex)
            {
                throw new CouldNotAddException(ex.Message);
            }
        }

        public async Task<LeaderBoard> Delete(int id)
        {
            try
            {
                var res = await Get(id);
                LeaderBoardContext.LeaderBoards.Remove(res);
                await LeaderBoardContext.SaveChangesAsync();
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception("Not able to delete Question");
            }
        }

        public async Task<LeaderBoard> Get(int id)
        {
            try
            {
                var LeaderBoard = await LeaderBoardContext.LeaderBoards.FirstOrDefaultAsync(e => e.LeaderBoardId == id);
                await LeaderBoardContext.SaveChangesAsync();
                if (LeaderBoard != null)
                {
                    return LeaderBoard;
                }
                throw new NotFoundException("LeaderBoard not found");

            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task<IEnumerable<LeaderBoard>> GetAll()
        {
            try
            {
                var LeaderBoard = await LeaderBoardContext.LeaderBoards.ToListAsync();
                await LeaderBoardContext.SaveChangesAsync();
                if (LeaderBoard != null)
                {
                    return LeaderBoard;
                }
                throw new NotFoundException("Collection empty");

            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task<LeaderBoard> Update(int id, LeaderBoard entity)
        {
            try
            {
                var existingLeaderBoard = await Get(id);

                if (entity == null)
                {
                    throw new CouldNotAddException("LeaderBoard entity is null.");
                }

                existingLeaderBoard.LeaderBoardName = entity.LeaderBoardName;
                existingLeaderBoard.Categories = entity.Categories;

                LeaderBoardContext.LeaderBoards.Update(existingLeaderBoard);
                await LeaderBoardContext.SaveChangesAsync();
                return existingLeaderBoard;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Failed to update LeaderBoard with ID {id}.");
                throw new CouldNotAddException($"Could not update LeaderBoard with ID {id}.");
            }
        }
    }
}
