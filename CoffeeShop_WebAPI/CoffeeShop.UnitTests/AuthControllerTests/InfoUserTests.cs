﻿using CoffeeShop.ServicesLogic.EntiteModels;
using CoffeeShop.ServicesLogic.Services;
using CoffeeShop_WebApi.Authorization.Models;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Controllers;
using Xunit;

namespace CoffeeShop.UnitTests.AuthControllerTests
{
    public class InfoUserTests
    {
        [Fact]
        public void HavingUserLogin_WhenGetInfoUser_IsSuccess()
        {
            //arrange
            var authenticateRequest = new AuthenticateRequest()
            {
                Email = "Maria.Ion@yahoo.com",
                Password = "123",
                Role = "User"
            };
            var response = new UserDto()
            {
                Email = "Maria.Ion@yahoo.com",
                FirstName = "Maria",
                LastName = "Ion",
                Role = "User",
                Password = "123"
            };
            var services = A.Fake<IServices<UserDto>>();
            A.CallTo(() => services.GetInfo(authenticateRequest)).Returns(response);
            var controller = new AuthController(services);

            //act
            var actionResult = controller.GetUserInfo(authenticateRequest);

            //assert
            var result = actionResult.Result as OkObjectResult;
            var resultMessage = result.Value as UserDto;
            Assert.Equal(response, resultMessage);
        }

        [Fact]
        public void HavingUserLogin_WhenGetInfoUser_IsFailes()
        {
            //arrange
            var authenticateRequest = new AuthenticateRequest()
            {
                Email = "Poenaru@gmail",
                Password = "21",
                Role = "User"
            };
            var services = A.Fake<IServices<UserDto>>();
            A.CallTo(() => services.GetInfo(authenticateRequest)).Returns(null);
            var controller = new AuthController(services);
            //act
            var actionResult = controller.GetUserInfo(authenticateRequest);

            //assert
            var result = actionResult.Result as BadRequestObjectResult;
            var resultMessage = result.Value as string;
            Assert.Equal("User doesn't exit!! \n You need to register this user", resultMessage);
        }
    }
}