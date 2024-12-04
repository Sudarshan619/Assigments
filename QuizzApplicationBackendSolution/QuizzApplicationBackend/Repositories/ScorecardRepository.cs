using Microsoft.EntityFrameworkCore;
using QuizzApplicationBackend.Context;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;

namespace QuizzApplicationBackend.Repositories
{
    public class ScorecardRepository : IRepository<int, ScoreCard>
    {
        private readonly QuizContext _context;
        private readonly ILogger<ScorecardRepository> _logger;

        public ScorecardRepository(QuizContext context, ILogger<ScorecardRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ScoreCard> Add(ScoreCard entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Scorecard entity cannot be null");

            try
            {
                await _context.ScoreCards.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a ScoreCard");
                throw new Exception("Failed to add ScoreCard");
            }
        }

        public async Task<ScoreCard> Delete(int id)
        {
            var scoreCard = await Get(id);
            if (scoreCard == null)
                throw new NotFoundException("ScoreCard not found");

            try
            {
                _context.ScoreCards.Remove(scoreCard);
                await _context.SaveChangesAsync();
                return scoreCard;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting ScoreCard");
                throw new Exception("Failed to delete ScoreCard");
            }
        }

        public async Task<ScoreCard> Get(int id)
        {
            try
            {
                var scoreCard = await _context.ScoreCards.FirstOrDefaultAsync(s => s.ScoreCardId == id);
                if (scoreCard == null)
                    throw new NotFoundException("ScoreCard not found");
                return scoreCard;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving ScoreCard");
                throw;
            }
        }

        public async Task<IEnumerable<ScoreCard>> GetAll()
        {
            try
            {
                var scoreCards = await _context.ScoreCards.ToListAsync();
                if (!scoreCards.Any())
                    throw new CollectionEmptyException("No scorecards found");
                return scoreCards;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all ScoreCards");
                throw;
            }
        }

        public async Task<ScoreCard> Update(int id, ScoreCard entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Scorecard entity cannot be null");

            var scoreCard = await Get(id);
            if (scoreCard == null)
                throw new NotFoundException("ScoreCard not found");

            try
            {
                scoreCard.QuizId = entity.QuizId;
                scoreCard.UserId = entity.UserId;
                scoreCard.Score = entity.Score;
                scoreCard.Acuuracy = entity.Acuuracy;

                _context.ScoreCards.Update(scoreCard);
                await _context.SaveChangesAsync();
                return scoreCard;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating ScoreCard");
                throw new Exception("Failed to update ScoreCard");
            }
        }
    }
}
