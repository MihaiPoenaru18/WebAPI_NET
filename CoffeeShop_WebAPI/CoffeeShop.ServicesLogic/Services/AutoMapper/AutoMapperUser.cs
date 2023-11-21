﻿using AutoMapper;
using CoffeeShop_WebApi.DataAccess.ModelDB;
using CoffeeShop.ServicesLogic.EntiteModels;
using CoffeeShop.DataAccess.DataAccess.ModelDB;
using CoffeeShop.ServicesLogic.EntiteModels.ModelsForProducts;

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
