using AutoMapper;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Models;

namespace QuizzApplicationBackend.Mappers
{
    public class QuizProfile:Profile
    {
        public QuizProfile()
        {
            CreateMap<Quiz, QuizDTO>();
            CreateMap<QuizDTO, Quiz>();
        }
    }
}
