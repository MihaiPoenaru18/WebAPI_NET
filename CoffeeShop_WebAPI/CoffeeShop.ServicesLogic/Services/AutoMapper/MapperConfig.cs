using AutoMapper;
using CoffeeShop_WebApi.Authorization.Models;
using CoffeeShop.ServicesLogic.EntiteModels;
using CoffeeShop.ServicesLogic.EntiteModels.ModelsForProducts;
using Serilog;
using CoffeeShop.DataAccess.DataAccess.ModelDB.User;
using CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel;
using CoffeeShop.DataAccess.DataAccess.ModelDB.Order;

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

                    if (typeof(Category) == typeof(Source) && typeof(CategoryDto) == typeof(Destination))
                    {
                        return MapperBetweenCategoryAndCategoryDto();
                    }

                    if (typeof(Order) == typeof(Source) && typeof(OrderDto) == typeof(Destination))
                    {
                        return MapperBetweenOrderAndOrderDto();
                    }

                    if (typeof(OrderDto) == typeof(Source) && typeof(Order) == typeof(Destination))
                    {
                        return MapperBetweenOrderDtoAndOrder();
                    }
                }

                return null;
            }
            catch (AutoMapperMappingException ex)
            {
                Log.Error($"MapperConfig<Source, Destination> -> InitializeAutomapper() -> Exception => {ex.Message}");
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
                Log.Error($"MapperConfig<Source, Destination> ->  MapperBetweenUserDtoAndUser() -> Exception => {ex.Message}");
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
                Log.Error($"MapperConfig<Source, Destination> -> MapperBetweenAuthenticateRequestAndUser() -> Exception => {ex.Message}");
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
                Log.Error($"MapperConfig<Source, Destination> -> MapperBetweenAuthenticateRequestAndUserDto( -> Exception => {ex.Message}");
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
                Log.Error($"MapperConfig<Source, Destination> -> MapperBetweenUserWithNewsLetterAndUserWithNewsLetterDto() -> Exception => {ex.Message}");
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
            catch (Exception ex)
            {
                Log.Error($"MapperConfig<Source, Destination> -> MapperBetweenUserWithNewsLetterDtoAndUserWithNewsLetter() -> Exception => {ex.Message}");
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
                                                        .ForMember(dest => dest.Category.Id, act => act.MapFrom(src => src.IdCategory))
                                                        .ForMember(dest => dest.Promotion.Id, act => act.MapFrom(src => src.IdPromotie))
                                                        .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                                                        .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                                                        .ForMember(dest => dest.Price, act => act.MapFrom(src => src.Price))
                                                        .ForMember(dest => dest.Currency, act => act.MapFrom(src => src.Currency))
                                                        .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                                                        .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
                                                        .ForMember(dest => dest.IsStock, act => act.MapFrom(src => src.IsStock))
                                                        .ForPath(dest => dest.Category.Id, act => act.MapFrom(src => src.Category.Id))
                                                        .ForPath(dest => dest.Category.Name, act => act.MapFrom(src => src.Category.Name))
                                                        .ForPath(dest => dest.Promotion.Id, act => act.MapFrom(src => src.Promotion.Id))
                                                        .ForPath(dest => dest.Promotion.PricePromotion, act => act.MapFrom(src => src.Promotion.PricePromotion))
                                                        .ForPath(dest => dest.Promotion.StartDate, act => act.MapFrom(src => src.Promotion.StartDate))
                                                        .ForPath(dest => dest.Promotion.EndDate, act => act.MapFrom(src => src.Promotion.EndDate));
                });
                return new Mapper(config);
            }
            catch (Exception ex)
            {
                Log.Error($"MapperConfig<Source, Destination> -> MapperBetweenProductAndProductDto() -> Exception => {ex.Message}");
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
                                                        .ForMember(dest => dest.IdCategory, act => act.MapFrom(src => src.Category.Id))
                                                        .ForMember(dest => dest.IdPromotie, act => act.MapFrom(src => src.Promotion.Id))
                                                        .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                                                        .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                                                        .ForMember(dest => dest.Price, act => act.MapFrom(src => src.Price))
                                                        .ForMember(dest => dest.Currency, act => act.MapFrom(src => src.Currency))
                                                        .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                                                        .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
                                                        .ForMember(dest => dest.IsStock, act => act.MapFrom(src => src.IsStock))
                                                        .ForPath(dest => dest.Category.Id, act => act.MapFrom(src => src.Category.Id))
                                                        .ForPath(dest => dest.Category.Name, act => act.MapFrom(src => src.Category.Name))
                                                        .ForPath(dest => dest.Promotion.Id, act => act.MapFrom(src => src.Promotion.Id))
                                                        .ForPath(dest => dest.Promotion.PricePromotion, act => act.MapFrom(src => src.Promotion.PricePromotion))
                                                        .ForPath(dest => dest.Promotion.StartDate, act => act.MapFrom(src => src.Promotion.StartDate))
                                                        .ForPath(dest => dest.Promotion.EndDate, act => act.MapFrom(src => src.Promotion.EndDate));
                });
                return new Mapper(config);
            }
            catch (Exception ex)
            {
                Log.Error($"MapperConfig<Source, Destination> -> MapperBetweenProductDtoAndProduct() -> Exception => {ex.Message}");
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
                                                         .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                                                        .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name));

                });
                return new Mapper(config);
            }
            catch (Exception ex)
            {
                Log.Error($"MapperConfig<Source, Destination> -> MapperBetweenCategoryAndCategoryDto() -> Exception => {ex.Message}");
                return null;
            }
        }
        public static Mapper MapperBetweenOrderAndOrderDto()
        {
            MapperConfiguration config;
            try
            {
                config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Order, OrderDto>().ForMember(dest => dest.Id, act => act.MapFrom(src => src.OrderId))
                                                        .ForMember(dest => dest.Currency, act => act.MapFrom(src => src.Currency))
                                                        .ForMember(dest => dest.TotalPrices, act => act.MapFrom(src => src.TotalPrices))
                                                        .ForMember(dest => dest.AddressId, act => act.MapFrom(src => src.AddressId))
                                                        .ForPath(dest => dest.Address.City, act => act.MapFrom(src => src.Address.City))
                                                        .ForPath(dest => dest.Address.Country, act => act.MapFrom(src => src.Address.Country))
                                                        .ForPath(dest => dest.Address.PostalCode, act => act.MapFrom(src => src.Address.PostalCode))
                                                        .ForPath(dest => dest.Products, act => act.MapFrom(src => src.Products));
                    cfg.CreateMap<Product, ProductDto>().ForMember(dest => dest.Sku, act => act.MapFrom(src => src.Sku))
                                                        .ForPath(dest => dest.Category.Id, act => act.MapFrom(src => src.IdCategory))
                                                        .ForPath(dest => dest.Promotion.Id, act => act.MapFrom(src => src.IdPromotie))
                                                        .ForPath(dest => dest.Id, act => act.MapFrom(src => src.Id))
                                                        .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                                                        .ForMember(dest => dest.Price, act => act.MapFrom(src => src.Price))
                                                        .ForMember(dest => dest.Currency, act => act.MapFrom(src => src.Currency))
                                                        .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                                                        .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
                                                        .ForMember(dest => dest.IsStock, act => act.MapFrom(src => src.IsStock))
                                                        .ForPath(dest => dest.Category.Id, act => act.MapFrom(src => src.Category.Id))
                                                        .ForPath(dest => dest.Category.Name, act => act.MapFrom(src => src.Category.Name))
                                                        .ForPath(dest => dest.Promotion.Id, act => act.MapFrom(src => src.Promotion.Id))
                                                        .ForPath(dest => dest.Promotion.PricePromotion, act => act.MapFrom(src => src.Promotion.PricePromotion))
                                                        .ForPath(dest => dest.Promotion.StartDate, act => act.MapFrom(src => src.Promotion.StartDate))
                                                        .ForPath(dest => dest.Promotion.EndDate, act => act.MapFrom(src => src.Promotion.EndDate));

                });
                return new Mapper(config);
            }
            catch (Exception ex)
            {
                Log.Error($"MapperConfig<Source, Destination> -> MapperBetweenOrderAndOrderDto() -> Exception => {ex.Message}");
                return null;
            }
        }

        public static Mapper MapperBetweenOrderDtoAndOrder()
        {
            MapperConfiguration config;
            try
            {
                config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<OrderDto, Order>()
                                                        .ForMember(dest => dest.OrderId, act => act.MapFrom(src => src.Id))
                                                        .ForMember(dest => dest.Currency, act => act.MapFrom(src => src.Currency))
                                                        .ForMember(dest => dest.AddressId, act => act.MapFrom(src => src.AddressId))
                                                        .ForMember(dest => dest.TotalPrices, act => act.MapFrom(src => src.TotalPrices))
                                                        .ForPath(dest => dest.Address.Id, act => act.MapFrom(src => src.AddressId))
                                                        .ForPath(dest => dest.Address.City, act => act.MapFrom(src => src.Address.City))
                                                        .ForPath(dest => dest.Address.Country, act => act.MapFrom(src => src.Address.Country))
                                                        .ForPath(dest => dest.Address.Street, act => act.MapFrom(src => src.Address.Street))
                                                        .ForPath(dest => dest.Address.Region, act => act.MapFrom(src => src.Address.Region))
                                                        .ForPath(dest => dest.Address.PostalCode, act => act.MapFrom(src => src.Address.PostalCode))
                                                        .ForPath(dest => dest.Products, act => act.MapFrom(src => src.Products));

                    cfg.CreateMap<ProductDto, Product>()
                                                        .ForMember(dest => dest.Sku, act => act.MapFrom(src => src.Sku))
                                                        .ForMember(dest => dest.IdCategory, act => act.MapFrom(src => src.Category.Id))
                                                        .ForMember(dest => dest.IdPromotie, act => act.MapFrom(src => src.Promotion.Id))
                                                        .ForMember(dest => dest.Id, act => act.MapFrom(src => src.Id))
                                                        .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                                                        .ForMember(dest => dest.Price, act => act.MapFrom(src => src.Price))
                                                        .ForMember(dest => dest.Currency, act => act.MapFrom(src => src.Currency))
                                                        .ForMember(dest => dest.Quantity, act => act.MapFrom(src => src.Quantity))
                                                        .ForMember(dest => dest.Description, act => act.MapFrom(src => src.Description))
                                                        .ForMember(dest => dest.IsStock, act => act.MapFrom(src => src.IsStock))
                                                        .ForPath(dest => dest.Category.Id, act => act.MapFrom(src => src.Category.Id))
                                                        .ForPath(dest => dest.Category.Name, act => act.MapFrom(src => src.Category.Name))
                                                        .ForPath(dest => dest.Promotion.Id, act => act.MapFrom(src => src.Promotion.Id))
                                                        .ForPath(dest => dest.Promotion.PricePromotion, act => act.MapFrom(src => src.Promotion.PricePromotion))
                                                        .ForPath(dest => dest.Promotion.StartDate, act => act.MapFrom(src => src.Promotion.StartDate))
                                                        .ForPath(dest => dest.Promotion.EndDate, act => act.MapFrom(src => src.Promotion.EndDate));

                });
                return new Mapper(config);
            }
            catch (Exception ex)
            {
                Log.Error($"MapperConfig<Source, Destination> -> MapperBetweenOrderDtoAndOrder() -> Exception => {ex.Message}");
                return null;
            }
        }
    }
}
