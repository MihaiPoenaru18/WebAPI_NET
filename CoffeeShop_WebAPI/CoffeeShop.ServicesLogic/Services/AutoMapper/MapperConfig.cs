using AutoMapper;
using CoffeeShop_WebApi.Authorization.Models;
using CoffeeShop_WebApi.DataAccess.ModelDB;
using CoffeeShop.ServicesLogic.EntiteModels;
using CoffeeShop.DataAccess.DataAccess.ModelDB;
using CoffeeShop.ServicesLogic.EntiteModels.ModelsForProducts;
using Serilog;

namespace CoffeeShop_WebApi.Services.AutoMapper
{
    public class MapperConfig<Source, Destination>
    {
        public static Mapper InitializeAutomapper()
        {
            MapperConfiguration config;
            try
            {
                if (typeof(Source) != typeof(Destination))
                {
                    if (typeof(User) == typeof(Source) && typeof(UserDto) == typeof(Destination))
                    {
                        return MapperBetweenUserAndUserDto();
                    }

                    if (typeof(UserDto) == typeof(Source) && typeof(User) == typeof(Destination))
                    {
                        return MapperBetweenUserDtoAndUser();
                    }

                    if (typeof(AuthenticateRequest) == typeof(Source) && typeof(User) == typeof(Destination))
                    {
                        return MapperBetweenAuthenticateRequestAndUser();
                    }

                    if (typeof(AuthenticateRequest) == typeof(Source) && typeof(UserDto) == typeof(Destination))
                    {
                        return MapperBetweenAuthenticateRequestAndUserDto();
                    }

                    if (typeof(UserWithNewsLetter) == typeof(Source) && typeof(UserWithNewsLetterDto) == typeof(Destination))
                    {
                        return MapperBetweenUserWithNewsLetterAndUserWithNewsLetterDto();
                    }

                    if (typeof(UserWithNewsLetterDto) == typeof(Source) && typeof(UserWithNewsLetter) == typeof(Destination))
                    {
                        return MapperBetweenUserWithNewsLetterDtoAndUserWithNewsLetter();
                    }
                    if (typeof(Product) == typeof(Source) && typeof(ProductDto) == typeof(Destination))
                    {
                        return MapperBetweenProductAndProductDto();
                    }
                    if (typeof(ProductDto) == typeof(Source) && typeof(Product) == typeof(Destination))
                    {
                        return MapperBetweenProductDtoAndProduct();
                    }
                    if (typeof(CategoryDto) == typeof(Source) && typeof(Category) == typeof(Destination))
                    {
                        return MapperBetweenCategoryAndCategoryDto();
                    }
                }

                return null;
            }
            catch (AutoMapperMappingException ex)
            {
                Log.Error($"ProductController -> GetProducts() -> Exception => {ex.Message}");
                return null;
            }
        }

        public static Mapper MapperBetweenUserAndUserDto()
        {
            MapperConfiguration config;

            config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, UserDto>().ForMember(dest => dest.Password, act => act.MapFrom(src => src.Password))
                                          .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
                                          .ForMember(dest => dest.FirstName, act => act.MapFrom(src => src.FirstName))
                                          .ForMember(dest => dest.LastName, act => act.MapFrom(src => src.LastName))
                                          .ForMember(dest => dest.Role, act => act.MapFrom(src => src.Role))
                                          .ForPath(dest => dest.NewsLetter.Email, act => act.MapFrom(src => src.UserWithNewsLetter.Email))
                                          .ForPath(dest => dest.NewsLetter.Name, act => act.MapFrom(src => src.UserWithNewsLetter.Name))
                                          .ForPath(dest => dest.NewsLetter.IsActived, act => act.MapFrom(src => src.UserWithNewsLetter.IsNewsLetterActive));
        });
            return new Mapper(config);
        }

        public static Mapper MapperBetweenUserDtoAndUser()
        {
            MapperConfiguration config;
            try
            {
                config = new MapperConfiguration(cfg =>
                {

                    cfg.CreateMap<UserDto, User>().ForMember(dest => dest.Password, act => act.MapFrom(src => src.Password))
                                                 .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
                                                 .ForMember(dest => dest.FirstName, act => act.MapFrom(src => src.FirstName))
                                                 .ForMember(dest => dest.LastName, act => act.MapFrom(src => src.LastName))
                                                 .ForMember(dest => dest.Role, act => act.MapFrom(src => src.Role))
                                                 .ForPath(dest => dest.UserWithNewsLetter.Email, act => act.MapFrom(src => src.NewsLetter.Email))
                                                 .ForPath(dest => dest.UserWithNewsLetter.Name, act => act.MapFrom(src => src.NewsLetter.Name))
                                                 .ForPath(dest => dest.UserWithNewsLetter.IsNewsLetterActive, act => act.MapFrom(src => src.NewsLetter.IsActived));
                });

                return new Mapper(config);
            }
            catch (Exception ex)
            {
                Log.Error($"ProductController -> GetProducts() -> Exception => {ex.Message}");
                return null;
            }
        }

        public static Mapper MapperBetweenAuthenticateRequestAndUser()
        {
            MapperConfiguration config;
            try
            {
                config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AuthenticateRequest, User>().ForMember(dest => dest.Password, act => act.MapFrom(src => src.Password))
                                                          .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
                                                          .ForMember(dest => dest.Role, act => act.MapFrom(src => src.Role));
            });

                return new Mapper(config);
            }
            catch (Exception ex)
            {
                Log.Error($"ProductController -> GetProducts() -> Exception => {ex.Message}");
                return null;
            }

        }

        public static Mapper MapperBetweenAuthenticateRequestAndUserDto()
        {
            MapperConfiguration config;
            try
            {
                config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AuthenticateRequest, UserDto>().ForMember(dest => dest.Password, act => act.MapFrom(src => src.Password))
                                                              .ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
                                                              .ForMember(dest => dest.Role, act => act.MapFrom(src => src.Role));
                });

                return new Mapper(config);
            }
            catch (Exception ex)
            {
                Log.Error($"ProductController -> GetProducts() -> Exception => {ex.Message}");
                return null;
            }
        }

        public static Mapper MapperBetweenUserWithNewsLetterAndUserWithNewsLetterDto()
        {
            MapperConfiguration config;
            try
            {
                config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<UserWithNewsLetter, UserWithNewsLetterDto>().ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
                                                                              .ForMember(dest => dest.IsActived, act => act.MapFrom(src => src.IsNewsLetterActive))
                                                              ;
                });
                return new Mapper(config);
            }
            catch (Exception ex)
            {
                Log.Error($"ProductController -> GetProducts() -> Exception => {ex.Message}");
                return null;
            }
        }

        public static Mapper MapperBetweenUserWithNewsLetterDtoAndUserWithNewsLetter()
        {
            MapperConfiguration config;
            try
            {
                config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<UserWithNewsLetterDto, UserWithNewsLetter>().ForMember(dest => dest.Email, act => act.MapFrom(src => src.Email))
                                                                              .ForMember(dest => dest.IsNewsLetterActive, act => act.MapFrom(src => src.IsActived));
                });
                return new Mapper(config);
            }
            catch(Exception ex) 
            {
                Log.Error($"ProductController -> GetProducts() -> Exception => {ex.Message}");
                return null;
            }
        }

        public static Mapper MapperBetweenProductAndProductDto()
        {
            MapperConfiguration config;
            try
            {
                config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Product, ProductDto>().ForMember(dest => dest.Sku, act => act.MapFrom(src => src.Sku))
                                                        .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                                                        .ForMember(dest => dest.Currency, act => act.MapFrom(src => src.Currency))
                                                        .ForMember(dest => dest.Price, act => act.MapFrom(src => src.Price))
                                                        .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                                                        .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
                                                        .ForMember(dest => dest.IsStock, act => act.MapFrom(src => src.IsStock))
                                                        .ForPath(dest => dest.Category.Name, act => act.MapFrom(src => src.Category.Name))
                                                        .ForPath(dest => dest.Promotion.PricePromotion, act => act.MapFrom(src => src.Promotion.PricePromotion))
                                                        .ForPath(dest => dest.Promotion.StartDate, act => act.MapFrom(src => src.Promotion.StartDate))
                                                        .ForPath(dest => dest.Promotion.EndDate, act => act.MapFrom(src => src.Promotion.EndDate));
                });
                return new Mapper(config);
            }
            catch (Exception ex)
            {
                Log.Error($"ProductController -> GetProducts() -> Exception => {ex.Message}");
                return null;
            }
        }

        public static Mapper MapperBetweenProductDtoAndProduct()
        {
            MapperConfiguration config;
            try
            {
                config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ProductDto, Product>().ForMember(dest => dest.Sku, act => act.MapFrom(src => src.Sku))
                                                        .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                                                        .ForMember(dest => dest.Price, act => act.MapFrom(src => src.Price))
                                                        .ForMember(dest => dest.Currency, act => act.MapFrom(src => src.Currency))
                                                        .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                                                        .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
                                                        .ForMember(dest => dest.IsStock, act => act.MapFrom(src => src.IsStock))
                                                        .ForPath(dest => dest.Category.Name, act => act.MapFrom(src => src.Category.Name))
                                                        .ForPath(dest => dest.Promotion.PricePromotion, act => act.MapFrom(src => src.Promotion.PricePromotion))
                                                        .ForPath(dest => dest.Promotion.StartDate, act => act.MapFrom(src => src.Promotion.StartDate))
                                                        .ForPath(dest => dest.Promotion.EndDate, act => act.MapFrom(src => src.Promotion.EndDate));
                });
                return new Mapper(config);
            }
            catch (Exception ex)
            {
                Log.Error($"ProductController -> GetProducts() -> Exception => {ex.Message}");
                return null;
            }
        }

        public static Mapper MapperBetweenCategoryAndCategoryDto()
        {
            MapperConfiguration config;
            try
            {
                config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Category, CategoryDto>()
                                                        .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name));
                });
                return new Mapper(config);
            }
            catch (Exception ex)
            {
                Log.Error($"ProductController -> GetProducts() -> Exception => {ex.Message}");
                return null;
            }
        }
    }
}
