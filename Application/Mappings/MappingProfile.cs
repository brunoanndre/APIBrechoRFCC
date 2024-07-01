using AutoMapper;
using APIBrechoRFCC.Core.Entities;
using APIBrechoRFCC.Application.DTO.InputDTOs;
using APIBrechoRFCC.Application.DTO.OutputDTOs;

namespace APIBrechoRFCC.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            //Category
            CreateMap<Category, CategoryOutputDTO>();
            CreateMap<CategoryInputDTO, Category>()
                .ForMember(dest => dest.Image,opt => opt.Ignore());
            

            //Product
            CreateMap<Product, ProductOutputDTO>();
            CreateMap<ProductInputDTO, Product>()
                .ForMember(p => p.Images, opt => opt.Ignore()); //Ignorar as imagens pois serão processadas separadamente

            //ProductOption
            CreateMap<ProductOption,ProductOptionOutputDTO>();
            CreateMap<ProductOptionInputDTO,ProductOption>();

            //ProductVariant
            CreateMap<ProductVariant, ProductVariantOutputDTO>();
            CreateMap<ProductVariantInputDTO, ProductVariant>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());

            //Home
            CreateMap<HomeBanner, HomeBannerOutputDTO>();
            CreateMap<HomeBannerInputDTO, HomeBanner>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());
            CreateMap<HomeSection, HomeSectionOutputDTO>();
            CreateMap<HomeSectionInputDTO, HomeSection>();

            //Order
            CreateMap<Order, OrderOutputDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
            CreateMap<OrderInputDTO, Order>();
            CreateMap<OrderItem, OrderItemOutputDTO>()
                .ForMember(dest => dest.ProductVariant, opt => opt.MapFrom(src => src.ProductVariant));
            CreateMap<OrderItemInputDTO, OrderItem>();
        }
    }
}
