using AutoMapper;
using eshop.Dtos.brand;
using eshop.Models;

namespace eshop.Profiles
{
    public class brandsProfile : Profile
    {
        public brandsProfile()
        {
            CreateMap<brands, brandsDto>()
                .ForMember(dest => dest.products, opt => opt.MapFrom(src => src.products));

            CreateMap<brandsPostDto, brands>();
        }
    }
}
