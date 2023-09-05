using AutoMapper;
using CoffeeShop_WebApi.Authorization.Models;
using CoffeeShop_WebApi.DataAccess.ModelDB;
using CoffeeShop.ServicesLogic.EntiteModels;

namespace CoffeeShop_WebApi.Services.AutoMapper
{
    public class MapperConfig<Source, Destination>
    {
        public static Mapper InitializeAutomapper()
        {
            MapperConfiguration config;
            if (typeof(Source) != typeof(Destination))
            {
                if (typeof(User) == typeof(Source) && typeof(UserDto) == typeof(Destination))
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

                if (typeof(UserDto) == typeof(Source) && typeof(User) == typeof(Destination))
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

                if (typeof(AuthenticateRequest) == typeof(Source) && typeof(User) == typeof(Destination))
                {
                    config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<AuthenticateRequest, User>().ForMember(dest => dest.Password, act => act.MapFrom(src => src.Password))
                                                                  .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
                                                                  .ForMember(dest => dest.Role, act => act.MapFrom(src => src.Role));
                    });

                    return new Mapper(config);
                }

                if (typeof(AuthenticateRequest) == typeof(Source) && typeof(UserDto) == typeof(Destination))
                {
                    config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<AuthenticateRequest, UserDto>().ForMember(dest => dest.Password, act => act.MapFrom(src => src.Password))
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
