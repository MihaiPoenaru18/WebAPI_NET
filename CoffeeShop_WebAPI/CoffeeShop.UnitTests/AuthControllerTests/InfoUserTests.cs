using CoffeeShop.ServicesLogic.EntiteModels;
using CoffeeShop.ServicesLogic.Services.Interfaces;
using CoffeeShop_WebApi.Authorization.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using CoffeeShop_WebApi.Controllers.User;
using Xunit;
using System.Threading.Tasks;

namespace CoffeeShop.UnitTests.AuthControllerTests
{
    public class InfoUserTests
    {
        [Fact]
        public async Task HavingUserLogin_WhenGetInfoUser_IsSuccess()
        {
            // Arrange
            var authenticateRequest = new AuthenticateRequest()
            {
                Email = "Maria.Ion@yahoo.com",
                Password = "123",
                Role = "User"
            };

            var expectedResponse = new UserDto()
            {
                Email = "Maria.Ion@yahoo.com",
                FirstName = "Maria",
                LastName = "Ion",
                Role = "User",
                Password = "123",
                NewsLetter = new UserWithNewsLetterDto
                {
                    Name = "Test",
                    Email = "Maria.Ion@yahoo.com",
                    IsActived = true
                }
            };

            var services = A.Fake<IServicesAuth<UserDto>>();
            A.CallTo(() => services.GetInfo(authenticateRequest)).Returns(Task.FromResult(expectedResponse));
            var controller = new AuthController(services);

            // Act
            var actionResult = await controller.GetUserInfo(authenticateRequest);

            // Assert
            var result = actionResult.Result as OkObjectResult;
            var resultMessage = result.Value as UserDto;
            Assert.NotNull(result);
            Assert.Equal(expectedResponse, resultMessage);
        }

        [Fact]
        public async Task HavingUserLogin_WhenGetInfoUser_Fails_ReturnsBadRequest()
        {
            // Arrange
            var authenticateRequest = new AuthenticateRequest()
            {
                Email = "Poenaru@gmail",
                Password = "21",
                Role = "User"
            };

            var services = A.Fake<IServicesAuth<UserDto>>();
            A.CallTo(() => services.GetInfo(authenticateRequest)).Returns(Task.FromResult<UserDto>(null));

            var controller = new AuthController(services);

            // Act
            var actionResult = await controller.GetUserInfo(authenticateRequest);

            // Assert
            var result = actionResult.Result as BadRequestObjectResult;
            Assert.NotNull(result);
            Assert.Equal("User doesn't exist! You need to register this user.", result.Value);
        }
    }
}
