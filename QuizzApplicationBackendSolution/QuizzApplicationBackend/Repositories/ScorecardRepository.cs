using Microsoft.EntityFrameworkCore;
using QuizzApplicationBackend.Context;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;

namespace QuizzApplicationBackend.Repositories
{
    public class ScorecardRepository : IRepository<int, ScoreCard>
    {
        private readonly QuizContext ScorecardContext;
        private readonly ILogger<ScorecardRepository> logger;

        public ScorecardRepository(QuizContext context, ILogger<ScorecardRepository> logger)
        {
            ScorecardContext = context;
            this.logger = logger;
        }

        public async Task<ScoreCard> Add(ScoreCard entity)
        {
            try
            {
                ScorecardContext.ScoreCards.Add(entity);
                await ScorecardContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Not able to add Scorecard");
            }
        }

        public async Task<ScoreCard> Delete(int id)
        {
            try
            {
                var res = await Get(id);
                ScorecardContext.ScoreCards.Remove(res);
                await ScorecardContext.SaveChangesAsync();
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception("Not able to delete Question");
            }
        }

        public async Task<ScoreCard> Get(int id)
        {
            try
            {
                var Scorecard = await ScorecardContext.ScoreCards.FirstOrDefaultAsync(e => e.ScoreCardId == id);
                await ScorecardContext.SaveChangesAsync();
                if (Scorecard != null)
                {
                    return Scorecard;
                }
                throw new NotFoundException("Scorecard not found");

            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task<IEnumerable<ScoreCard>> GetAll()
        {
            try
            {
                var scoreCards = await ScorecardContext.ScoreCards.ToListAsync();
                if (!scoreCards.Any())
                {
                    throw new CollectionEmptyException("No scorecards available.");
                }

                return scoreCards;
            }
            catch (CollectionEmptyException ex)
            {
                throw new CollectionEmptyException(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while retrieving scorecards: " + ex.Message);
            }
        }
        public async Task<ScoreCard> Update(int id, ScoreCard entity)
        {
            try
            {
                var scoreCard = await Get(id);

                scoreCard.QuizId = entity.QuizId;
                scoreCard.UserId = entity.UserId;
                scoreCard.Score = entity.Score;

                var result = ScorecardContext.ScoreCards.Update(scoreCard);
                await ScorecardContext.SaveChangesAsync();

                return scoreCard;
            }
            catch (Exception ex) { 
                
                throw new Exception(ex.Message);
            }
        }
    }
}
