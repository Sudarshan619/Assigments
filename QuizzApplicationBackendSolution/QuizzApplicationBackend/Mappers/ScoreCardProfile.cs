using AutoMapper;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Models;

namespace ScoreCardzApplicationBackend.Mappers
{
    public class ScoreCardProfile:Profile
    {
        public ScoreCardProfile()
        {
            CreateMap<ScoreCard, ScoreCardDTO>();
            CreateMap<ScoreCard, ScoreCardResponseDTO>();
            CreateMap<ScoreCardResponseDTO, ScoreCard>();
            CreateMap<ScoreCardDTO, ScoreCard>();
        }
    }
}
