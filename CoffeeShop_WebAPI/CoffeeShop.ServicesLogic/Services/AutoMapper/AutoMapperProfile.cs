using AutoMapper;
using CoffeeShop.ServicesLogic.EntiteModels;
using CoffeeShop.ServicesLogic.EntiteModels.ModelsForProducts;
using CoffeeShop.DataAccess.DataAccess.ModelDB.UserModels;
using CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel;
using CoffeeShop.DataAccess.DataAccess.ModelDB.OrderModels;
using CoffeeShop_WebApi.Authorization.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CoffeeShop.ServicesLogic.Services.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.NewsLetter, opt => opt.MapFrom(src => src.UserWithNewsLetter))
                .ReverseMap();

            CreateMap<UserWithNewsLetter, UserWithNewsLetterDto>()
                .ForMember(dest => dest.IsActived, opt => opt.MapFrom(src => src.IsNewsLetterActive))
                .ReverseMap();

            CreateMap<AuthenticateRequest, User>();

            CreateMap<Product, ProductDto>()
                .ForPath(dest => dest.Category.Name, opt => opt.MapFrom(src => src.Category.Name))
                .ForPath(dest => dest.Promotion, opt => opt.MapFrom(src => src.Promotion))
                .ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.OrderId))
                .ReverseMap();

            CreateMap<Promotion, PromotionDto>().ReverseMap();
        }
    }

    public static class AutoMapperConfig
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            return services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
