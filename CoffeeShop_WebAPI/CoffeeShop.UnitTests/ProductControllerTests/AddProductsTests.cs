using AutoMapper;
using CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel;
using CoffeeShop.DataAccess.DataAccess.Repository.Interfaces;
using CoffeeShop.ServicesLogic.EntiteModels.ModelsForProducts;
using CoffeeShop.ServicesLogic.Services;
using CoffeeShop.ServicesLogic.Services.Interfaces;
using CoffeeShop_WebApi.Controllers.Product;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CoffeeShop.UnitTests.ProductControllerTests
{
    public class AddProductsTests
    {
        private readonly IMapper _mapper;
        private readonly ICoffeeShopProductsRepository<Product> _productRepository;
        private readonly ServicesProducts _services;

        public AddProductsTests()
        {
            _mapper = A.Fake<IMapper>();
            _productRepository = A.Fake<ICoffeeShopProductsRepository<Product>>();
            _services = new ServicesProducts(_productRepository, _mapper);
        }

        [Fact]
        public async Task AddProducts_WhenProductIsNull_ReturnsFalse()
        {
            // Arrange
            List<ProductDto> products = null;

            // Act
            var result = await _services.AddNewProducts(products);

            // Assert
            Assert.False(result);
            A.CallTo(() => _productRepository.Insert(A<Product>._)).MustNotHaveHappened();
        }

        [Fact]
        public async Task AddProducts_WhenProductsProvided_AddsSuccessfully()
        {
            // Arrange
            var products = new List<ProductDto>
        {
            new ProductDto { Name = "Espresso", Price = 10, Quantity = 100 }
        };

            var mappedProduct = new Product { Name = "Espresso", Price = 10, Quantity = 100 };

            A.CallTo(() => _productRepository.GetAll()).Returns(new List<Product>());
            A.CallTo(() => _mapper.Map<Product>(A<ProductDto>._)).Returns(mappedProduct);

            // Act
            var result = await _services.AddNewProducts(products);

            // Assert
            Assert.True(result);
            A.CallTo(() => _productRepository.Insert(A<Product>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task AddProducts_WhenProductExists_DoesNotAddProduct()
        {
            // Arrange
            var products = new List<ProductDto>
        {
            new ProductDto { Name = "Espresso", Price = 10, Quantity = 100 }
        };

            A.CallTo(() => _productRepository.GetAll()).Returns(new List<Product>
        {
            new Product { Name = "Espresso" }
        });

            // Act
            var result = await _services.AddNewProducts(products);

            // Assert
            Assert.True(result);
            A.CallTo(() => _productRepository.Insert(A<Product>._)).MustNotHaveHappened();
        }
    }
}
