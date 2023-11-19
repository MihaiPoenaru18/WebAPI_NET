using Xunit;
using FakeItEasy;
using CoffeeShop.ServicesLogic.EntiteModels;
using WebApplication1.Controllers;
using Microsoft.AspNetCore.Mvc;
using CoffeeShop.ServicesLogic.Services.Interfaces;

namespace CoffeeShop.UnitTests.AuthControllerTests
{
    public class RegisterControllerTests
    {
        [Fact]
        public void HavingUnregisterUser_WhenUserIsRegister_ThenAddNewUserWithSuccess()
        {
            //arrange
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
            var messageOk = "Register Success";
            var services = A.Fake<IServicesAuth<UserDto>>();
            A.CallTo(() => services.IsUserRegistered(requestUser)).Returns(true);
            var controller = new AuthController(services);
            //act
            var actionResult = controller.Register(requestUser);
            //Assert
            var result = actionResult as OkObjectResult;
            var resultMessage = result.Value as string;
            Assert.Equal(messageOk, resultMessage);
        }

        [Fact]
        public void HavingRegisterUser_WhenUserIsRegister_ThenAddNewUserWithFailes()
        {
            //arrange
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
            var badMessage = "The user already exist!!!";
            var services = A.Fake<IServicesAuth<UserDto>>();
            A.CallTo(() => services.IsUserRegistered(requestUser)).Returns(false);
            var controller = new AuthController(services);

            //act
            var actionResult = controller.Register(requestUser);

            //Assert
            var result = actionResult as BadRequestObjectResult;
            var resultMessage = result.Value as string;
            Assert.Equal(badMessage, resultMessage);
        }

        [Fact]
        public void HavingRegisterUser_WhenUserIsNull_ThenAddNewUserWithFailes()
        {
            //arrange
            var badMessage = "The fiels are emplty!!!";
            var services = A.Fake<IServicesAuth<UserDto>>();
            A.CallTo(() => services.IsUserRegistered(null)).Returns(false);
            var controller = new AuthController(services);

            //act
            var actionResult = controller.Register(null);

            //Assert
            var result = actionResult as BadRequestObjectResult;
            var resultMessage = result.Value as string;
            Assert.Equal(badMessage, resultMessage);
        }

    }
}