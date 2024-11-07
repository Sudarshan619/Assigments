using AutoMapper;
using Day27_Webapi_EF.DTO;
using Day27_Webapi_EF.Models;

namespace Day27_Webapi_EF.Mapper
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.PricePerUnit, opt => opt.MapFrom(src => src.Price));
            CreateMap<ProductDTO,Product>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.PricePerUnit));
        }
    }
}
