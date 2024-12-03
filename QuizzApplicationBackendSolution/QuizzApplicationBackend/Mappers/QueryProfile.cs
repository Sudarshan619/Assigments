using AutoMapper;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Models;

namespace QuizzApplicationBackend.Mappers
{
    public class QueryProfile:Profile
    {
        public QueryProfile()
        {
            CreateMap<Query, QueryDTO>();
            CreateMap<QueryResponseDTO, Query>();
            CreateMap<Query, QueryResponseDTO>();

            CreateMap<QueryDTO, Query>();
        }
    }
}
