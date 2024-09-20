using CoffeeShop.ServicesLogic.EntiteModels;
using Xunit;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using CoffeeShop.ServicesLogic.Services.Interfaces;
using CoffeeShop_WebApi.Controllers.User;
using System.Threading.Tasks;

namespace CoffeeShop.UnitTests.NewsletterControllerTests
{
    public class AddUserToNewsletterTest
    {
        private readonly IServicesNewsLetter<UserWithNewsLetterDto> _fakeService;
        private readonly NewsLetterController _controller;
        private UserWithNewsLetterDto bodyNewsLetter = new UserWithNewsLetterDto()
        {
            Email = "Ana-Maria@gmail.com",
            Name = "Ion Marian",
            IsActived = true,
        };

        public AddUserToNewsletterTest()
        {
            _fakeService = A.Fake<IServicesNewsLetter<UserWithNewsLetterDto>>();
            _controller = new NewsLetterController(_fakeService);
        }

        [Fact]
        public async Task HavingUser_WhenWantAddUserToNewsletter_IsSuccess()
        {
            // Arrange
            var bodyNewsLetter = new UserWithNewsLetterDto()
            {
                Email = "Ana-MariaIoana@gmail.com",
                Name = "Ana-Maria Ioana",
                IsActived = true,
            };

            A.CallTo(() => _fakeService.IsUserRegisteredWithNewsLetter(bodyNewsLetter))
                .Returns(Task.FromResult(false));  // Simulate user is not subscribed

            // Act
            var actionsResult = await _controller.AddUserToNewsLetter(bodyNewsLetter);

            // Assert
            var result = actionsResult.Result as OkObjectResult;
            var resultMessage = result.Value as string;
            Assert.Equal("Subscriber Success", resultMessage);
        }

        [Fact]
        public async Task HavingUser_WhenWantAddUserToNewsletter_IsAlreadyUseNewsletter()
        {
            // Arrange
            A.CallTo(() => _fakeService.IsUserRegisteredWithNewsLetter(bodyNewsLetter))
                .Returns(Task.FromResult(true));  // Simulate user is already subscribed

            // Act
            var actionsResult = await _controller.AddUserToNewsLetter(bodyNewsLetter);

            // Assert
            var result = actionsResult.Result as BadRequestObjectResult;
            var resultMessage = result.Value as string;
            Assert.Equal("The user already subscribed to the newsletter!!!", resultMessage);
        }

        [Fact]
        public async Task HavingUser_WhenWantAddUserToNewsletter_TheBodyIsNull()
        {
            // Arrange
            var bodyNewsLetter = new UserWithNewsLetterDto();  // Simulating an empty DTO

            // Act
            var actionsResult = await _controller.AddUserToNewsLetter(bodyNewsLetter);

            // Assert
            var result = actionsResult.Result as BadRequestObjectResult;
            var resultMessage = result.Value as string;
            Assert.Equal("The fields are empty!!!", resultMessage);
        }
    }
}
