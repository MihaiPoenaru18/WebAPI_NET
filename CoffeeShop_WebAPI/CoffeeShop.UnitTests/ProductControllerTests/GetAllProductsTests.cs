using AutoMapper;
using CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel;
using CoffeeShop.DataAccess.DataAccess.Repository.Interfaces;
using CoffeeShop.ServicesLogic.EntiteModels.ModelsForProducts;
using CoffeeShop.ServicesLogic.Services;
using FakeItEasy;
using Xunit;

namespace CoffeeShop.UnitTests.ProductControllerTests
{
    public class GetAllProductsTests
    {
        private readonly IMapper _mapper;
        private readonly ICoffeeShopProductsRepository<Product> _productRepository;
        private readonly ServicesProducts _services;

        public GetAllProductsTests()
        {
            _mapper = A.Fake<IMapper>();
            _productRepository = A.Fake<ICoffeeShopProductsRepository<Product>>();
            _services = new ServicesProducts(_productRepository, _mapper);
        }

        [Fact]
        public async Task GetAllProducts_WhenProductsExist_ReturnsProductList()
        {
            // Arrange
            var productList = new List<Product>
        {
            new Product { Name = "Espresso", Price = 10, Quantity = 100 }
        };

            A.CallTo(() => _productRepository.GetAll()).Returns(Task.FromResult((IEnumerable<Product>)productList));
            A.CallTo(() => _mapper.Map<IEnumerable<ProductDto>>(productList)).Returns(new List<ProductDto>
        {
            new ProductDto { Name = "Espresso", Price = 10, Quantity = 100 }
        });

            // Act
            var result = await _services.GetAllProducts();

            // Assert
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetAllProducts_WhenNoProductsExist_ReturnsEmptyList()
        {
            // Arrange
            var emptyProductList = new List<Product>();
            A.CallTo(() => _productRepository.GetAll()).Returns(Task.FromResult((IEnumerable<Product>)emptyProductList));

            // Act
            var result = await _services.GetAllProducts();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetAllProducts_WhenExceptionOccurs_ReturnsEmptyList()
        {
            // Arrange
            A.CallTo(() => _productRepository.GetAll()).Throws(new System.Exception());

            // Act
            var result = await _services.GetAllProducts();

            // Assert
            Assert.Empty(result);
        }
    }
}
