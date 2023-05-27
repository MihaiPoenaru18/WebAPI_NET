using AutoMapper;
using CoffeeShop_WebApi.Authorization.Models;
using CoffeeShop_WebApi.DataAccess.ModelDB;
using CoffeeShop_WebApi.EntiteModels;

namespace CoffeeShop_WebApi.Services.AutoMapper
{
    public class MapperConfig<T1, T2>
    {
        public static Mapper InitializeAutomapper()
        {
            MapperConfiguration config;
            if (typeof(T1) != typeof(T2))
            {
                if (typeof(User) == typeof(T1) && typeof(UserDto) == typeof(T2))
                {
                    config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<User, UserDto>().ForMember(dest => dest.Password, act => act.MapFrom(src => src.Password))
                                                      .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
                                                      .ForMember(dest => dest.FirstName, act => act.MapFrom(src => src.FirstName))
                                                      .ForMember(dest => dest.LastName, act => act.MapFrom(src => src.LastName))
                                                      .ForMember(dest => dest.Role, act => act.MapFrom(src => src.Role));
                    });

                    return new Mapper(config);
                }

                if (typeof(UserDto) == typeof(T1) && typeof(User) == typeof(T2))
                {
                    config = new MapperConfiguration(cfg =>
                    {

                        cfg.CreateMap<UserDto, User>().ForMember(dest => dest.Password, act => act.MapFrom(src => src.Password))
                                                     .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
                                                     .ForMember(dest => dest.FirstName, act => act.MapFrom(src => src.FirstName))
                                                     .ForMember(dest => dest.LastName, act => act.MapFrom(src => src.LastName))
                                                     .ForMember(dest => dest.Role, act => act.MapFrom(src => src.Role));
                    });

                    return new Mapper(config);
                }

                if (typeof(AuthenticateRequest) == typeof(T1) && typeof(User) == typeof(T2))
                {
                    config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<AuthenticateRequest, User>().ForMember(dest => dest.Password, act => act.MapFrom(src => src.Password))
                                                                  .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
                                                                  .ForMember(dest => dest.Role, act => act.MapFrom(src => src.Role));
                    });

                    return new Mapper(config);
                }
            }
            return null;
        }
    }
}
