using AutoMapper;
using CoffeeShop.DataAccess.DataAccess.ModelDB;
using CoffeeShop.DataAccess.DataAccess.Repository.Interfaces;
using CoffeeShop.ServicesLogic.EntiteModels;
using CoffeeShop.ServicesLogic.EntiteModels.ModelsForProducts;
using CoffeeShop.ServicesLogic.Services.Interfaces;
using CoffeeShop_WebApi.DataAccess.ModelDB;
using CoffeeShop_WebApi.Services.AutoMapper;
using Serilog;
using System.Collections.Generic;

namespace CoffeeShop.ServicesLogic.Services
{
    public class ServicesProducts : IServicesProduct<ProductDto>
    {
        private readonly IMapper _mapper;
        private readonly ICoffeeShopRepository<Product> _coffeeShopProductRepository;

        public ServicesProducts(ICoffeeShopRepository<Product> coffeeShopProductRepository, IMapper mapper)
        {
            _mapper = mapper;
            _coffeeShopProductRepository = coffeeShopProductRepository;
        }
        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            var mappeProducts = MapperConfig<Product, ProductDto>.InitializeAutomapper();
            var productsDto = new List<ProductDto>();
            try
            {
                var products = await _coffeeShopProductRepository.GetAll();
                if (products != null || products.Count() > 0)
                {
                    foreach (var product in products)
                    {
                        productsDto.Add(mappeProducts.Map<Product, ProductDto>(product));
                    }
                }
                return productsDto;
            }
            catch (Exception ex)
            {
                Log.Error("ServicesProducts  -> GetAllProducts() -> Exception => {@ex.Message}", ex.Message);
            }
            return null;
        }

        public void UpdateProductInformation(ProductDto product)
        {
            try
            {
                var mappeProducts = MapperConfig<ProductDto, Product>.InitializeAutomapper();
                _coffeeShopProductRepository.Update(mappeProducts.Map<ProductDto, Product>(product));
            }
            catch (Exception ex)
            {
                Log.Error("ServicesProducts  -> UpdateProductInformation() -> Exception => {@ex.Message}", ex.Message);
            }
        }

        public async Task<bool> IsProductExistingInDb(string productName)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(productName))
                {
                    var products = await _coffeeShopProductRepository.GetAll();
                    if (products.Where(p => p.Name == productName).FirstOrDefault() != null)
                    {
                        return true;
                    }
                }
                else
                {
                    throw new Exception("Product Name is null");
                }
                return false;
            }
            catch (Exception ex)
            {
                Log.Error("ServicesProducts  -> IsProductExistingInDb() -> Exception => {@ex.Message}", ex.Message);
                return false;
            }
        }

        public async Task AddNewProductsInDBAsync(List<ProductDto> products)
        {
            var mappeProducts = MapperConfig<ProductDto, Product>.InitializeAutomapper();
            try
            {
                if (products != null)
                {
                    foreach (var product in products)
                    {
                        if (await IsProductExistingInDb(product.Name))
                        {
                            _coffeeShopProductRepository.Insert(mappeProducts.Map<ProductDto, Product>(product));
                        }
                    }
                }
                else
                {
                    throw new NullReferenceException("Product Name is null");
                }

            }
            catch (Exception ex)
            {
                Log.Error("ServicesProducts  -> GetAllUsers() -> Exception => {@ex.Message}", ex.Message);
            }
        }
    }
}
