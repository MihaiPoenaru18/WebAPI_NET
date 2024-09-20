using CoffeeShop.ServicesLogic.Authorization;
using CoffeeShop_WebApi.Authorization.Models;
using CoffeeShop.ServicesLogic.EntiteModels;
using Microsoft.AspNetCore.Mvc;
using CoffeeShop.ServicesLogic.Services.Interfaces;
using Serilog;

namespace CoffeeShop_WebApi.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IServicesAuth<UserDto> _services;

        public AuthController(IServicesAuth<UserDto> services)
        {
            _services = services;
        }

        [HttpPost("RegisterUser")]
        public async Task<ActionResult> Register(UserDto request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Role))
            {
                return BadRequest("Fields are empty!");
            }

            try
            {
                var isRegistered = await _services.IsUserRegistered(request);
                if (isRegistered)
                {
                    return BadRequest("The user already exists!");
                }

                return Ok("Register Success");
            }
            catch (Exception ex)
            {
                Log.Error("Error during user registration: {Message}", ex.Message);
                return BadRequest("An error occurred while processing the request.");
            }
        }

        [HttpPost("Authenticate")]
        public async Task<ActionResult<AuthenticateResponse>> Login([FromBody] AuthenticateRequest authenticateRequest)
        {
            try
            {
                var response = await _services.Authenticate(authenticateRequest);
                if (response == null)
                {
                    return BadRequest("Email or password is incorrect");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error("Error during login: {Message}", ex.Message);
                return BadRequest("An error occurred while processing the request.");
            }
        }

        [HttpPost("GetUserInfo")]
        public async  Task<ActionResult<UserDto>> GetUserInfo([FromBody] AuthenticateRequest authenticateRequest)
        {
            try
            {
                var userInfo = await _services.GetInfo(authenticateRequest);
                if (userInfo == null)
                {
                    return BadRequest("User doesn't exist! You need to register this user.");
                }

                return Ok(userInfo);
            }
            catch (Exception ex)
            {
                Log.Error("Error retrieving user info: {Message}", ex.Message);
                return BadRequest("An error occurred while processing the request.");
            }
        }

        [AllowAnonymous]
        [HttpGet("GetAllUsersInfo"), Authorize]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsersInfo([FromBody] AuthenticateRequest loginUser)
        {
            if (loginUser.Role != "Admin")
            {
                return BadRequest("You are not authorized for this request!");
            }

            try
            {
                var users = await  _services.GetAllUsers();
                if (users == null || !users.Any())
                {
                    return BadRequest("No users found! You need to register users.");
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                Log.Error("Error retrieving all users info: {Message}", ex.Message);
                return BadRequest("An error occurred while processing the request.");
            }
        }
    }
}
