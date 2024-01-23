using CoffeeShop.ServicesLogic.EntiteModels.ModelsForProducts;
using CoffeeShop.ServicesLogic.Services.Interfaces;
using CoffeeShop_WebApi.Controllers.Product;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CoffeeShop.UnitTests.ProductControllerTests
{
    public class AddProductsTests
    {
        //[Fact]
        //public void HavingProducts_WhenAddProducrts_WithSuccess()
        //{
        //    //arange
        //    var body = new List<ProductDto>() {
        //       new ProductDto
        //    {
        //        Name = "Product 1",
        //        Sku = "ABC123",
        //        Description = "This is a sample product",
        //        Currency = "USD",
        //        Price = 50,
        //        Quantity = 10,
        //        IsStock = true,
        //        Promotion = new PromotionDto
        //        {
        //            PricePromotion= 10,
        //            StartDate = DateTime.Now,
        //            EndDate = DateTime.Now.AddDays(2),
        //        },
        //        Category = new CategoryDto
        //        {
        //            Name = "Category",
        //        }
        //    },
        //         new ProductDto
        //    {
        //        Name = "Product 2",
        //        Sku = "ABC123",
        //        Description = "This is a sample product",
        //        Currency = "USD",
        //        Price = 50,
        //        Quantity = 10,
        //        IsStock = true,
        //        Promotion = new PromotionDto
        //        {
        //            PricePromotion= 10,
        //            StartDate = DateTime.Now,
        //            EndDate = DateTime.Now.AddDays(2),
        //        },
        //        Category = new CategoryDto
        //        {
        //            Name = "Category",
        //        }
        //    }
        //    };
        //    var services = A.Fake<IServicesProduct<ProductDto>>();
        //    A.CallTo(() => services.AddNewProducts(body));
        //    var controller = new ProductController(services);

        //    //act
        //    var actionsResult = controller.AddProducts(body);

        //    //assert
        //    var result = actionsResult as OkObjectResult;
        //    var resultMessage = result.Value as string;
        //    Assert.Equal("Products add in DB with success", resultMessage);
        //}

    }
}
