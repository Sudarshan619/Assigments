using AutoMapper;
using QuizzApplicationBackend.Models;
using QuizzApplicationBackend.DTO;

namespace QuizzApplicationBackend.Mappers
{
    public class QuestionProfile:Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionDTO>();
            CreateMap<QuestionDTO, Question>();
        }
    }
}
