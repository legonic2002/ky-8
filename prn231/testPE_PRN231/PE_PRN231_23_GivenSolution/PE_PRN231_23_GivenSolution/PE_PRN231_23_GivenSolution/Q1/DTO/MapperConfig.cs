using AutoMapper;
using LuyenDePRN231.DTO;
using Q1.Models;

namespace Q1.DTO
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Product, ProductDTO>()
                   .ForMember(x => x.CategoryName, y => y
                   .MapFrom(src => src.Category.CategoryName))
                   .ReverseMap();

        }
    }
}
