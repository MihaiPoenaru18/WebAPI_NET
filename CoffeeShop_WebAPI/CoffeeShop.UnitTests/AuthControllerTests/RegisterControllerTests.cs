using Xunit;
using FakeItEasy;
using CoffeeShop.ServicesLogic.EntiteModels;
using CoffeeShop_WebApi.Controllers.User;
using Microsoft.AspNetCore.Mvc;
using CoffeeShop.ServicesLogic.Services.Interfaces;
using System.Threading.Tasks;

namespace CoffeeShop.UnitTests.AuthControllerTests
{
    public class RegisterControllerTests
    {
        [Fact]
        public async Task HavingUnregisteredUser_WhenUserRegisters_ThenSuccessMessageReturned()
        {
            // Arrange
            var requestUser = new UserDto()
            {
                Email = "Mihai@gm",
                FirstName = "ion",
                LastName = "maria",
                Role = "User",
                Password = "123",
                NewsLetter = new UserWithNewsLetterDto
                {
                    Name = "Test",
                    Email = "Mihai@gm",
                    IsActived = true
                }
            };
            var expectedMessage = "Register Success";
            var services = A.Fake<IServicesAuth<UserDto>>();
            A.CallTo(() => services.IsUserRegistered(requestUser)).Returns(false);  // User is not registered

            var controller = new AuthController(services);

            // Act
            var actionResult = await controller.Register(requestUser);

            // Assert
            var result = actionResult as OkObjectResult;
            var resultMessage = result?.Value as string;
            Assert.Equal(expectedMessage, resultMessage);
        }

        [Fact]
        public async Task HavingRegisteredUser_WhenUserRegisters_ThenFailMessageReturned()
        {
            // Arrange
            var requestUser = new UserDto()
            {
                Email = "Maria.Ion@yahoo.com",
                FirstName = "Maria",
                LastName = "Ion",
                Role = "User",
                Password = "123",
                NewsLetter = new UserWithNewsLetterDto
                {
                    Name = "Test",
                    Email = "Mihai@gm",
                    IsActived = true
                }
            };
            var expectedMessage = "The user already exists!";
            var services = A.Fake<IServicesAuth<UserDto>>();
            A.CallTo(() => services.IsUserRegistered(requestUser)).Returns(true);  // User is already registered

            var controller = new AuthController(services);

            // Act
            var actionResult = await controller.Register(requestUser);

            // Assert
            var result = actionResult as BadRequestObjectResult;
            var resultMessage = result?.Value as string;
            Assert.Equal(expectedMessage, resultMessage);
        }

        [Fact]
        public async Task HavingNullUser_WhenUserRegisters_ThenFieldsEmptyMessageReturned()
        {
            // Arrange
            var expectedMessage = "Fields are empty!";
            var services = A.Fake<IServicesAuth<UserDto>>();

            var controller = new AuthController(services);

            // Act
            var actionResult = await controller.Register(null);

            // Assert
            var result = actionResult as BadRequestObjectResult;
            var resultMessage = result?.Value as string;
            Assert.Equal(expectedMessage, resultMessage);
        }
    }
}
