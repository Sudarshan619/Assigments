using AutoMapper;
using Day27_Webapi_EF.DTO;
using Day27_Webapi_EF.Models;

namespace Day27_Webapi_EF.Mapper
{
    public class CartItemProfile : Profile
    {
        public CartItemProfile()
        {
            CreateMap<CartItem, CartItemDTO>();
            CreateMap<CartItemDTO, CartItem>();
        }
    }
}
