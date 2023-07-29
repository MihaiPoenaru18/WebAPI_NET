﻿using CoffeeShop_WebApi.Authorization;
using CoffeeShop_WebApi.Authorization.Models;
using CoffeeShop_WebApi.DataAccess.ModelDB;
using CoffeeShop_WebApi.EntiteModels;
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

        [HttpPost("RegisterUser")]
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

        [AllowAnonymous]
        [HttpGet("Authenticate")]
        public ActionResult<AuthenticateResponse> Login([FromBody] AuthenticateRequest authenticateRequest)
        {
            var response = _services.Authenticate(authenticateRequest);

            if (response == null)
            {
                return BadRequest("Username or password is incorrect");
            }

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("GetUserInfo"), Authorize]
        public ActionResult<UserDto> GetUserInfo([FromBody] AuthenticateRequest authenticateRequest)
        {
            if (_services.GetInfo(authenticateRequest) == null)
            {
                return BadRequest("User doesn't exit!! \n You need to register this user");
            }
            return Ok(_services.GetInfo(authenticateRequest));
        }

        [AllowAnonymous]
        [HttpGet("GetAllUsersInfo"), Authorize]
        public ActionResult<IEnumerable<User>> GetAllUsersInfoo([FromQuery] AuthenticateRequest loginUser)
        {
            if (loginUser.Role == "Admin")
            {
                if (_services.GetInfo(loginUser) == null)
                {
                    return BadRequest("User doesn't exit!! \n You need to register this user");
                }
                return Ok(_services.GetInfo(loginUser));
            }
            return BadRequest("You are not authorised for this request!!!");
        }
    }
}
