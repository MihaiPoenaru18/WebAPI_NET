using CoffeeShop.ServicesLogic.EntiteModels;
using Xunit;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using CoffeeShop_WebApi.Controllers;
using CoffeeShop.ServicesLogic.Services.Interfaces;
using CoffeeShop_WebApi.Controllers.User;

namespace CoffeeShop.UnitTests.NewsletterControllerTests
{
    public class AddUserToNewsletterTest
    {
        [Fact]
        public void HavingUser_WhenWantAddUserToNewsletter_IsSuccess()
        {
            //arange
            var bodyNewsLetter = new UserWithNewsLetterDto()
            {
                Email = "Ana-Maria@gmail.com",
                Name = "Ion Marian",
                IsActived = true,
            };

            var services = A.Fake<IServicesNewsLetter<UserWithNewsLetterDto>>();
            A.CallTo(() => services.IsUserRegisteredWithNewsLetter(bodyNewsLetter));
            var controller = new NewsLetterController(services);

            //act
            var actionsResult = controller.AddUserToNewsLetter(bodyNewsLetter);

            //assert
            var result = actionsResult.Result as OkObjectResult;
            var resultMessage = result.Value as string;
            Assert.Equal("Subscriber Success", resultMessage);
        }

        [Fact]
        public void HavingUser_WhenWantAddUserToNewsletter_IsAlreadyUseNewsletter()
        {
            //arange
            var bodyNewsLetter = new UserWithNewsLetterDto()
            {
                Email = "ion@gmail.com",
                IsActived = true,
                Name = "ion"
            };
            var services = A.Fake<IServicesNewsLetter<UserWithNewsLetterDto>>();
            A.CallTo(() => services.IsUserRegisteredWithNewsLetter(bodyNewsLetter));
            var controller = new NewsLetterController(services);
            //act
            var actionsResult = controller.AddUserToNewsLetter(bodyNewsLetter);

            //assert
            var result = actionsResult.Result as BadRequestObjectResult;
            var resultMessage = result.Value as string;
            Assert.Equal("The user already subscribed to the newsletter!!!", resultMessage);
        }

        [Fact]
        public void HavingUser_WhenWantAddUserToNewsletter_TheBodyIsNull()
        {
            //arange
            var bodyNewsLetter = new UserWithNewsLetterDto();
            var services = A.Fake<IServicesNewsLetter<UserWithNewsLetterDto>>();
            A.CallTo(() => services.IsUserRegisteredWithNewsLetter(bodyNewsLetter));
            var controller = new NewsLetterController(services);
            //act
            var actionsResult = controller.AddUserToNewsLetter(bodyNewsLetter);

            //assert
            var result = actionsResult.Result as BadRequestObjectResult;
            var resultMessage = result.Value as string;
            Assert.Equal("The fields are empty!!!", resultMessage);
        }


    }

}
