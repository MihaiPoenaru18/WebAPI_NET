using AutoMapper;
using CoffeeShop.ServicesLogic.EntiteModels;
using CoffeeShop.ServicesLogic.EntiteModels.ModelsForProducts;
using CoffeeShop.DataAccess.DataAccess.ModelDB.User;
using CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel;

namespace CoffeeShop.ServicesLogic.Services.AutoMapper
{
    public class AutoMapperUser : Profile
    {
        public AutoMapperUser()
        {
            CreateMap<UserDto, User>();

            CreateMap<User, UserDto>();

            CreateMap<UserWithNewsLetter, UserWithNewsLetterDto>();

            CreateMap<UserWithNewsLetterDto, UserWithNewsLetter>();

            CreateMap<Product, ProductDto>();

            CreateMap<ProductDto, Product>();

            CreateMap<Category, CategoryDto>();
        }
    }
}
