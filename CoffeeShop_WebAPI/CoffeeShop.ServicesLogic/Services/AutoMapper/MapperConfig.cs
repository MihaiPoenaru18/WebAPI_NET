using AutoMapper;
using CoffeeShop_WebApi.DataAccess.ModelDB;
using CoffeeShop_WebApi.EntiteModels;

namespace CoffeeShop_WebApi.Services.AutoMapper
{
    public  class MapperConfig
    {
        public static Mapper InitializeAutomapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>().ForMember(dest => dest.Password, act => act.MapFrom(src => src.Password));
            });
            return new Mapper(config);
        }
    }
}
