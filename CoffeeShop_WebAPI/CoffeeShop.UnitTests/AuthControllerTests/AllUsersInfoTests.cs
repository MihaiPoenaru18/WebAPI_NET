using CoffeeShop.ServicesLogic.EntiteModels;
using CoffeeShop.ServicesLogic.Services.Interfaces;
using CoffeeShop_WebApi.Authorization.Models;
using CoffeeShop_WebApi.Controllers.User;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;

using Xunit;

namespace CoffeeShop.UnitTests.AuthControllerTests
{
    public class AllUsersInfoTests
    {
        [Fact]
        public void HavingAdminLogin_WhenGetAllUsersInfo_IsSuccess()
        {
            //arrange
            var authenticateRequest = new AuthenticateRequest()
            {
                Email = "Poenaru@gmail",
                Password = "21",
                Role = "Admin"
            };

            var fakeUsers = new List<UserDto>() 
            { new UserDto()
            { Email = "Poenaru@gmail", 
             FirstName = "Test",
             LastName = "Test",
             Password = "123",
             Role = "User"
            } };
            var services = A.Fake<IServicesAuth<UserDto>>();
            A.CallTo(() => services.GetAllUsers()).Returns(fakeUsers);
            var controller = new AuthController(services);
            //act
            var actionResult = controller.GetAllUsersInfo(authenticateRequest);

            //assert
            var result = actionResult.Result as OkObjectResult;
            var resultInfo = result.Value as List<UserDto>;
            Assert.Equal(1, resultInfo.Count);
        }

        [Fact]
        public void HavingAdminLogin_WhenGetAllUsersInfo_IsFailes()
        {
            //arrange
            var authenticateRequest = new AuthenticateRequest()
            {
                Email = "Poenaru@gmail",
                Password = "21",
                Role = "Admin",
                
            };
            var services = A.Fake<IServicesAuth<UserDto>>();
            A.CallTo(() => services.GetAllUsers()).Returns(null);
            var controller = new AuthController(services);
            //act
            var actionResult = controller.GetAllUsersInfo(authenticateRequest);


            // Assert
            //assert
            var result = actionResult.Result as BadRequestObjectResult;
            var resultMessage = result.Value as string;
            Assert.Equal("User doesn't exit!! \n You need to register this user", resultMessage);
        }

        [Fact]
        public void GetAllUsersInfo_WhenNoUsers_ReturnsBadRequest()
        {
            // Arrange
            var authenticateRequest = new AuthenticateRequest()
            {
                Email = "Poenarugmail",
                Password = "21",
                Role = "User"
            };

            var services = A.Fake<IServicesAuth<UserDto>>();
            A.CallTo(() => services.GetAllUsers()).Returns(null);

            var controller = new AuthController(services);

            // Act
            var actionResult = controller.GetAllUsersInfo(authenticateRequest);

            // Assert
            var result = actionResult.Result as BadRequestObjectResult;
            var resultMessage = result.Value as string;
            Assert.Equal("You are not authorised for this request!!!", resultMessage);
        }

    }
}
