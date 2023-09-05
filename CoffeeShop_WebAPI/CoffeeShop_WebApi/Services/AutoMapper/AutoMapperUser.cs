using AutoMapper;
using CoffeeShop_WebApi.Models;
using WebApplication1.Models;

namespace CoffeeShop_WebApi.Services.AutoMapper
{
    public class AutoMapperUser : Profile
    {
        public AutoMapperUser() 
        {
            CreateMap<UserDto, User>();

            CreateMap<User,UserDto>();
        }
    }
}
