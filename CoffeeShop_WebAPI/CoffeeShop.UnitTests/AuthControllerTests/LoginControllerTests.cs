using Xunit;
using FakeItEasy;
using CoffeeShop.ServicesLogic.EntiteModels;
using Microsoft.AspNetCore.Mvc;
using CoffeeShop_WebApi.Authorization.Models;
using CoffeeShop.ServicesLogic.Services.Interfaces;
using CoffeeShop_WebApi.Controllers.User;
using System.Threading.Tasks;

namespace CoffeeShop.UnitTests.AuthControllerTests
{
    public class LoginControllerTests
    {
        [Fact]
        public async Task HavingUser_WhenLoginFails_ThenReturnBadRequest()
        {
            // Arrange
            var authenticateRequest = new AuthenticateRequest()
            {
                Email = "Poenaru@gmail",
                Password = "21",
                Role = "User",
            };

            var services = A.Fake<IServicesAuth<UserDto>>();
            A.CallTo(() => services.Authenticate(authenticateRequest)).Returns(Task.FromResult<AuthenticateResponse>(null)); // Simulate failed authentication

            var controller = new AuthController(services);

            // Act
            var actionResult = await controller.Login(authenticateRequest);

            // Assert
            var result = actionResult.Result as BadRequestObjectResult;
            Assert.NotNull(result);
            Assert.Equal("Email or password is incorrect", result.Value);
        }

        [Fact]
        public async Task HavingUser_WhenLoginSucceeds_ThenReturnOkResult()
        {
            // Arrange
            var authenticateRequest = new AuthenticateRequest()
            {
                Email = "Poenaru@gmail",
                Password = "21",
                Role = "User"
            };

            var authenticateResponse = new AuthenticateResponse(authenticateRequest, "fake-token")
            {
                Email = "Poenaru@gmail",
                CreatedDate = DateTime.Now,
                ExpiresDate = DateTime.Now.AddDays(1),
                Token = "fake-token",
            };

            var services = A.Fake<IServicesAuth<UserDto>>();
            A.CallTo(() => services.Authenticate(authenticateRequest)).Returns(Task.FromResult(authenticateResponse)); // Simulate successful authentication

            var controller = new AuthController(services);

            // Act
            var actionResult = await controller.Login(authenticateRequest);

            // Assert
            var result = actionResult.Result as OkObjectResult;
            Assert.NotNull(result);
            Assert.Equal(authenticateResponse, result.Value);
        }
    }
}
