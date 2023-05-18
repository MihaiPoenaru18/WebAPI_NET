using AutoMapper;
using Azure.Core;
using CoffeeShop_WebApi.Authorization;
using CoffeeShop_WebApi.Authorization.Models;
using CoffeeShop_WebApi.DataAccess.ModelDB;
using CoffeeShop_WebApi.Models;
using CoffeeShop_WebApi.Services;

using Microsoft.AspNetCore.Mvc;


namespace WebApplication1.Controllers
{
    [Route("api/AuthController/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IServices<UserDto> _services;

        public AuthController(IServices<UserDto> services)
        {
            _services = services;
        }

        [HttpPost("Register")]
        public ActionResult<User> Register(UserDto request)
        {
            if (request == null)
            {
                return BadRequest("The fiels are emplty!!!");
            }
            if (!_services.IsUserRegistered(request).Result)
            {
                return BadRequest("The user already exist!!!");
            }
            return Ok("Register Success");
        }

        [HttpGet("Authenticate")]
        public ActionResult<AuthenticateResponse> Login([FromQuery] AuthenticateRequest authenticateRequest)
        {
            var findUser = _services.GetAllUsers().SingleOrDefault(x => x.Email == authenticateRequest.Email);
            var response = _services.Authenticate(authenticateRequest);

            if (!BCrypt.Net.BCrypt.Verify(authenticateRequest.Password, findUser.Password) && response == null && findUser == null)
            {
                return BadRequest("Username or password is incorrect");
            }

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("GetUserInfo"), Authorize]
        public ActionResult<UserDto> GetUserInfo([FromQuery] AuthenticateRequest loginUser)
        {
            return Ok(_services.GetInfo(loginUser));
        }

        [AllowAnonymous]
        [HttpGet("GetAllUsersInfo"), Authorize]
        public ActionResult<IEnumerable<User>> GetAllUsersInfoo([FromQuery] AuthenticateRequest loginUser)
        {
            if (loginUser.Role == "Admin")
            {
                if (_services.GetAllUsers() == null)
                {
                    return BadRequest("Database is empty!!!");
                }
                return Ok(_services.GetAllUsers().OrderBy(x => x.FirstName).ToList());
            }

            return BadRequest("You are not authorised for this request!!!");
        }
    }
}
