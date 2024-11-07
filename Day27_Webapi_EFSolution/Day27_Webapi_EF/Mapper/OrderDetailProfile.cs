using Day27_Webapi_EF.DTO;
using Day27_Webapi_EF.Models;
using AutoMapper;

namespace Day27_Webapi_EF.Mapper
{
    public class OrderDetailProfile : Profile
    {
        public OrderDetailProfile()
        {
            CreateMap<OrderDetail, OrderDetailDTO>();
            CreateMap<OrderDetailDTO, OrderDetail>();
        }
    }
}
