using AutoMapper;
using CoffeeShop_WebApi.DataAccess.ModelDB;
using CoffeeShop.ServicesLogic.EntiteModels;

namespace CoffeeShop.ServicesLogic.Services.AutoMapper
{
    public class AutoMapperUser : Profile
    {
        public AutoMapperUser()
        {
            CreateMap<UserDto, User>();

            CreateMap<User, UserDto>();

        }
    }
}
