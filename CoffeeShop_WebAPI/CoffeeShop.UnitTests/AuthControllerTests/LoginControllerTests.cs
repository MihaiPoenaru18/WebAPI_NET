using Xunit;
using FakeItEasy;
using CoffeeShop.ServicesLogic.Services;
using CoffeeShop.ServicesLogic.EntiteModels;
using WebApplication1.Controllers;
using Microsoft.AspNetCore.Mvc;
using CoffeeShop_WebApi.Authorization.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CoffeeShop.UnitTests.AuthControllerTests
{
    public class LoginControllerTests
    {
        [Fact]
        public void HavingUser_WhenLogin_ThenRequestIsIncorresct()
        {
            //arrange
            var authenticateRequest = new AuthenticateRequest()
            {
                Email = "Poenaru@gmail",
                Password = "21",
                Role = "User"
            };
            var services = A.Fake<IServicesAuth<UserDto>>();
            A.CallTo(() => services.Authenticate(authenticateRequest)).Returns(null);
            var controller =new AuthController(services);
            //act
            var actionResult = controller.Login(authenticateRequest);
            //assert
            var result = actionResult.Result as BadRequestObjectResult;
            var resultMessage = result.Value as string;
            Assert.Equal("Username or password is incorrect",resultMessage);
        }

        [Fact]
        public void HavingUser_WhenLogin_ThenRequestIsCorresct()
        {
            //arrange
            var authenticateRequest = new AuthenticateRequest()
            {
                Email = "Poenaru@gmail",
                Password = "21",
                Role = "User"
            };
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Email, authenticateRequest.Email),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "User"),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF32.GetBytes("ion"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);
          
            var authenticateResponse = new AuthenticateResponse(authenticateRequest,token.ToString())
            {
                Email = "Poenaru@gmail",
                CreatedDate = DateTime.Now,
                ExpiresDate = DateTime.Now.AddDays(1),
                Token = token.ToString(),
             };
            var services = A.Fake<IServicesAuth<UserDto>>();
            A.CallTo(() => services.Authenticate(authenticateRequest)).Returns(authenticateResponse);
            var controller = new AuthController(services);

            //act
            var actionResult = controller.Login(authenticateRequest);

            //assert
            var result = actionResult.Result as OkObjectResult;
            var resultMessage = result.Value as AuthenticateResponse;
            Assert.Equal(authenticateResponse, resultMessage);
        }
    }
}
