using AutoMapper;
using CoffeeShop_WebApi.Model;
using WebApplication1.Model;

namespace CoffeeShop_WebApi.Services.AutoMapper
{
    public class AutoMapperUser : Profile
    {
        public AutoMapperUser() 
        {
            CreateMap<UserDto, User>();
        }
    }
}
