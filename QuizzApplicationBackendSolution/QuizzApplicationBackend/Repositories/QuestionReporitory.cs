using Microsoft.EntityFrameworkCore;
using QuizzApplicationBackend.Context;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;

namespace QuizzApplicationBackend.Repositories
{
    public class QuestionRepository : IRepository<int, Question>
    {
        private readonly QuizContext QuestionContext;
        private readonly ILogger<QuestionRepository> logger;

        public QuestionRepository(QuizContext context, ILogger<QuestionRepository> logger)
        {
            QuestionContext = context;
            this.logger = logger;
        }

        public async Task<Question> Add(Question entity)
        {
            try
            {
                QuestionContext.Questions.Add(entity);
                await QuestionContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Not able to add Question");
            }
        }

        public async Task<Question> Delete(int id)
        {
            try
            {
                var res = await Get(id);
                QuestionContext.Questions.Remove(res);
                await QuestionContext.SaveChangesAsync();
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception("Not able to delete Question");
            }
        }

        public async Task<Question> Get(int id)
        {
            try
            {
                var policy = await QuestionContext.Questions.FirstOrDefaultAsync(e => e.QuestionId == id);
                await QuestionContext.SaveChangesAsync();
                if (policy != null)
                {
                    return policy;
                }
                throw new NotFoundException("Question not found");

            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task<IEnumerable<Question>> GetAll()
        {
            try
            {
                var policy = await QuestionContext.Questions.ToListAsync();
                await QuestionContext.SaveChangesAsync();
                if (policy.Count !=0 )
                {
                    return policy;
                }
                throw new CollectionEmptyException("Collection empty");

            }
            catch (CollectionEmptyException ex)
            {
                throw new CollectionEmptyException(ex.Message);
            }
        }

        public async Task<Question> Update(int id, Question entity)
        {
            try
            {
                
                var existingQuestion = await Get(id);

               
                existingQuestion.QuestionName = entity.QuestionName;
                existingQuestion.Difficulty = entity.Difficulty;
                existingQuestion.Points = entity.Points;
                existingQuestion.Options = entity.Options; 

                await QuestionContext.SaveChangesAsync();

                return existingQuestion;
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Not able to update Question");
            }
        }

    }
}
