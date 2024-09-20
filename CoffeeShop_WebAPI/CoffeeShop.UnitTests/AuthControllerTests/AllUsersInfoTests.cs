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
        public async Task HavingAdminLogin_WhenGetAllUsersInfo_IsSuccess()
        {
            // Arrange
            var authenticateRequest = new AuthenticateRequest()
            {
                Email = "Poenaru@gmail",
                Password = "21",
                Role = "Admin"
            };

            var fakeUsers = new List<UserDto>
            {
                new UserDto
                {
                    Email = "Poenaru@gmail",
                    FirstName = "Test",
                    LastName = "Test",
                    Password = "123",
                    Role = "User"
                }
            };

            var services = A.Fake<IServicesAuth<UserDto>>();
            A.CallTo(() => services.GetAllUsers()).Returns(await Task.FromResult(fakeUsers)); // Async return
            var controller = new AuthController(services);

            // Act
            var actionResult = await controller.GetAllUsersInfo(authenticateRequest); // Await async method

            // Assert
            var result = actionResult.Result as OkObjectResult;
            var resultInfo = result.Value as List<UserDto>;
            Assert.Single(resultInfo);
        }

        [Fact]
        public async Task HavingAdminLogin_WhenGetAllUsersInfo_IsFails()
        {
            // Arrange
            var authenticateRequest = new AuthenticateRequest()
            {
                Email = "Poenaru@gmail",
                Password = "21",
                Role = "Admin"
            };

            var nullUsers = new List<UserDto>(); // Empty list, simulating no users
            var services = A.Fake<IServicesAuth<UserDto>>();
            A.CallTo(() => services.GetAllUsers()).Returns(await Task.FromResult(nullUsers)); // Async return
            var controller = new AuthController(services);

            // Act
            var actionResult = await controller.GetAllUsersInfo(authenticateRequest);

            // Assert
            var result = actionResult.Result as BadRequestObjectResult;
            var resultMessage = result.Value as string;
            Assert.Equal("No users found! You need to register users.", resultMessage);
        }

        [Fact]
        public async Task GetAllUsersInfo_WhenNoAdminRole_ReturnsBadRequest()
        {
            // Arrange
            var authenticateRequest = new AuthenticateRequest()
            {
                Email = "Poenarugmail",
                Password = "21",
                Role = "User" // Not an admin
            };

            var listOfUsers = new List<UserDto>();
            var services = A.Fake<IServicesAuth<UserDto>>();
            A.CallTo(() => services.GetAllUsers()).Returns(await Task.FromResult(listOfUsers)); // Async return

            var controller = new AuthController(services);

            // Act
            var actionResult = await controller.GetAllUsersInfo(authenticateRequest);

            // Assert
            var result = actionResult.Result as BadRequestObjectResult;
            var resultMessage = result.Value as string;
            Assert.Equal("You are not authorized for this request!", resultMessage);
        }
    }
}
