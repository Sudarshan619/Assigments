using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Models;

namespace QuizzApplicationBackend.Interfaces
{
    public interface IQuizService<K, T> where K : class
    {
        public Task<bool> CreateQuiz(QuizDTO question);
        public Task<bool> DeleteQuiz(T id);
        public Task<bool> EditQuiz(T id, QuizDTO question);

        public Task<IEnumerable<QuizQuestionReponseDTO>> GetAllQuizzesWithQuestions();
        public Task<K> GetRandomQuestionsByCategory(Categories category, int noOfQuestions);
        public Task<QuizQuestionReponseDTO> GetQuiz(int id);
    }
}
