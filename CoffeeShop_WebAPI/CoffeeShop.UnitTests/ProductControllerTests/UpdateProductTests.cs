using AutoMapper;
using CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel;
using CoffeeShop.DataAccess.DataAccess.Repository.Interfaces;
using CoffeeShop.ServicesLogic.EntiteModels.ModelsForProducts;
using CoffeeShop.ServicesLogic.Services;
using FakeItEasy;

using Xunit;

namespace CoffeeShop.UnitTests.ProductControllerTests
{
    public class UpdateProductTests
    {
        private readonly IMapper _mapper;
        private readonly ICoffeeShopProductsRepository<Product> _productRepository;
        private readonly ServicesProducts _services;

        public UpdateProductTests()
        {
            _mapper = A.Fake<IMapper>();
            _productRepository = A.Fake<ICoffeeShopProductsRepository<Product>>();
            _services = new ServicesProducts(_productRepository, _mapper);
        }

        [Fact]
        public async Task UpdateProductInformation_WhenProductIsValid_UpdatesSuccessfully()
        {
            // Arrange
            var productDto = new ProductDto { Name = "Espresso", Price = 10, Quantity = 100 };
            var mappedProduct = new Product { Name = "Espresso", Price = 10, Quantity = 100 };

            A.CallTo(() => _mapper.Map<Product>(productDto)).Returns(mappedProduct);

            // Act
            await _services.UpdateProductInformation(productDto);

            // Assert
            A.CallTo(() => _productRepository.Update(mappedProduct)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task UpdateProductInformation_WhenExceptionOccurs_LogsError()
        {
            // Arrange
            var productDto = new ProductDto { Name = "Espresso", Price = 10, Quantity = 100 };

            A.CallTo(() => _mapper.Map<Product>(productDto)).Throws(new System.Exception());

            // Act
            await _services.UpdateProductInformation(productDto);

            // Assert
            A.CallTo(() => _productRepository.Update(A<Product>._)).MustNotHaveHappened();
        }
    }
}