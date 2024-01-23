using CoffeeShop.ServicesLogic.EntiteModels.ModelsForProducts;
using CoffeeShop.ServicesLogic.Services.Interfaces;
using CoffeeShop_WebApi.Controllers.Product;
using CoffeeShop_WebApi.QuerybleCustomMethod.ModelOfParameters;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CoffeeShop.UnitTests.ProductControllerTests
{
    public class GetAllProductsTests
    {
        //[Fact]
        //public void HavingProducts_WhenAddProducrts_WithSuccess()
        //{
        //    //arange
        //    ProductQueryParameters productQueryParameters = new ProductQueryParameters();

        //    var services = A.Fake<IServicesProduct<ProductDto>>();
        //    A.CallTo(() => services.GetAllProducts());
        //    var controller = new ProductController(services);

        //    //act
        //    var actionsResult = controller.GetProducts(productQueryParameters);

        //    //assert
        //    var result = actionsResult.Result as OkObjectResult;
        //    var resultMessage = result.Value as List<ProductDto>;
        //    Assert.Equal(1, resultMessage.Count);
        //}
    }
}
