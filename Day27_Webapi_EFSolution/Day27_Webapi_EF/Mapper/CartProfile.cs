using AutoMapper;
using Day27_Webapi_EF.DTO;
using Day27_Webapi_EF.Models;

namespace Day27_Webapi_EF.Mapper
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<Cart, CartDTO>();
            CreateMap<CartDTO, Cart>();
        }
    }
}
