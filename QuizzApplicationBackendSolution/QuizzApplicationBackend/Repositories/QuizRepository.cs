using Microsoft.EntityFrameworkCore;
using QuizzApplicationBackend.Context;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;
using Microsoft.Extensions.Logging;

namespace QuizzApplicationBackend.Repositories
{
    public class QuizRepository : IRepository<int, Quiz>
    {
        private readonly QuizContext quizContext;
        private readonly ILogger<QuizRepository> _logger;

        public QuizRepository(QuizContext context, ILogger<QuizRepository> logger)
        {
            quizContext = context;
            _logger = logger;
        }
        
        public async Task<Quiz> Add(Quiz entity)
        {
            try
            {
                quizContext.Quizes.Add(entity);
                await quizContext.SaveChangesAsync();
                return entity;
            }
            catch(Exception ex)
            {
                throw new Exception("Not able to add quiz");
            }
        }

        public async Task<Quiz> Delete(int id)
        {
            try
            {
                var res = await Get(id);
                quizContext.Quizes.Remove(res);
                await quizContext.SaveChangesAsync();
                return res;
            }
            catch (Exception ex)
            {
                throw new Exception("Not able to delete Question");
            }
        }

        public async Task<Quiz> Get(int id)
        {
            try
            {
                var Quiz = await quizContext.Quizes.FirstOrDefaultAsync(e => e.QuizId == id);
                await quizContext.SaveChangesAsync();
                if (Quiz != null)
                {
                    return Quiz;
                }
                throw new NotFoundException("Quiz not found");

            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task<IEnumerable<Quiz>> GetAll()
        {
            try
            {
                var Quiz = await quizContext.Quizes.ToListAsync();
                await quizContext.SaveChangesAsync();
                if (Quiz.Count != 0)
                {
                    return Quiz;
                }
                throw new CollectionEmptyException("Collection empty");

            }
            catch (CollectionEmptyException ex)
            {
                throw new CollectionEmptyException(ex.Message);
            }
        }

        public async Task<Quiz> Update(int id, Quiz entity)
        {
            try
            {
                var quiz = await Get(id);
                quiz.Category = entity.Category;
                quiz.Difficulty = entity.Difficulty;
                quiz.NoOfQuestions = entity.NoOfQuestions;
                quiz.Title = entity.Title;

                var result = quizContext.Quizes.Update(quiz);
                await quizContext.SaveChangesAsync();

                return quiz;

            }
            catch(Exception ex) { 
              
                throw new Exception(ex.Message);
            }
        }
    }
}
