using AutoMapper;
using CoffeeShop_WebApi.DataAccess.ModelDB;
using CoffeeShop_WebApi.EntiteModels;

namespace CoffeeShop_WebApi.Services.AutoMapper
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
