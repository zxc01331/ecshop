using AutoMapper;
using eshop.Dtos.brand;
using eshop.Dtos.categories;
using eshop.Dtos.Categories;
using eshop.Models;
namespace eshop.Profiles
{
    public class categoriesProfile : Profile
    {
        public categoriesProfile() 
        {
            CreateMap<categories, categoriesDto>()
                .ForMember(dest => dest.products, opt => opt.MapFrom(src => src.products));

            CreateMap<categoriesPostDto, categories>();
        } 
    }
}
