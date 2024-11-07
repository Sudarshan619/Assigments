using AutoMapper;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Models;

namespace QuizzApplicationBackend.Mappers
{
    public class LeaderBoardProfile:Profile
    {
        public LeaderBoardProfile()
        {
            CreateMap<LeaderBoardDTO, LeaderBoard>();
            CreateMap<LeaderBoard, LeaderBoardDTO>();
        }
    }
}
