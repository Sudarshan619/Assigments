using AutoMapper;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Models;

namespace QuizzApplicationBackend.Mappers
{
    public class OptionProfile:Profile
    {
        public OptionProfile() {
            CreateMap<Option, OptionDTO>();
            CreateMap<OptionDTO, Option>();
        }
            
    }
}
