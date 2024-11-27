using AutoMapper;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Models;

namespace QuizzApplicationBackend.Mappers
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User, LoginResponseDTO>();
            CreateMap<LoginResponseDTO, User>();
            CreateMap<User, LoginResponseDTO>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Name));
        }
    }
}
