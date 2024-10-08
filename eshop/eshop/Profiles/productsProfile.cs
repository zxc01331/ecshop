using AutoMapper;
using eshop.Dtos;
using eshop.Dtos.Product_images;
using eshop.Dtos.Products;
using eshop.Models;

namespace eshop.Profiles
{
    public class productsProfile : Profile
    {
        public  productsProfile() 
        {
            CreateMap<products, productsDto>()
            .ForMember(dest => dest.product_images, opt => opt.MapFrom(src => src.product_images))
            .ForMember(dest => dest.brand_name, opt => opt.MapFrom(src => src.brand!.brand_name))// 映射 brand.brand_name
            .ForMember(dest => dest.category_name, opt => opt.MapFrom(src => src.category!.category_name));// 映射 category.category_name
            CreateMap<product_images, product_imagesDto>();

            CreateMap<productsPostDto, products>()
            .ForMember(dest => dest.created_at, opt => opt.Ignore())
            .ForMember(dest => dest.updated_at, opt => opt.Ignore())
            .ForMember(dest => dest.product_images, opt => opt.MapFrom(src => src.product_images));
            
            CreateMap<product_imagesPostDto, product_images>()
            .ForMember(dest => dest.id, opt => opt.Ignore())// 忽略 id 的映射
            .ForMember(dest => dest.product, opt => opt.Ignore()); // 避免循環引用

            CreateMap<productsPutDto, products>();

            CreateMap<product_imagesPutDto, product_images>();

        }
    }
}
