using CoffeeShop.ServicesLogic.Authorization;
using CoffeeShop_WebApi.Authorization.Models;
using CoffeeShop_WebApi.DataAccess.ModelDB;
using CoffeeShop.ServicesLogic.EntiteModels;
using Microsoft.AspNetCore.Mvc;
using CoffeeShop.ServicesLogic.Services.Interfaces;
using Serilog;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IServicesAuth<UserDto> _services;

        public AuthController(IServicesAuth<UserDto> services)
        {
            _services = services;
        }

        [HttpPost("RegisterUser")]
        public ActionResult Register(UserDto request)
        {
            try
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
            catch (Exception ex)
            {
                Log.Information("AutoController -> Register -> Exception => {@ex.Message}", ex.Message);
                return BadRequest("Error");
            }
        }

        [HttpPost("Authenticate")]
        public ActionResult<AuthenticateResponse> Login([FromBody] AuthenticateRequest authenticateRequest)
        {
            var response = _services.Authenticate(authenticateRequest);
            try
            {
                if (response == null)
                {
                    return BadRequest("Email or password is incorrect");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {

                Log.Information("AutoController -> Login() -> Exception => {@ex.Message}", ex.Message);
                return BadRequest("Error");
            }

        }

        [HttpPost("GetUserInfo")]
        public ActionResult<UserDto> GetUserInfo([FromBody] AuthenticateRequest authenticateRequest)
        {
            try
            {
                if (_services.GetInfo(authenticateRequest) == null)
                {
                    return BadRequest("User doesn't exit!! \n You need to register this user");
                }
                return Ok(_services.GetInfo(authenticateRequest));
            }
            catch (Exception ex)
            {
                Log.Information("AutoController -> GetUserInfo() -> Exception => {@ex.Message}", ex.Message);
                return BadRequest("Error");
            }
        }

        [AllowAnonymous]
        [HttpGet("GetAllUsersInfo"), Authorize]
        public ActionResult<IEnumerable<User>> GetAllUsersInfo([FromBody] AuthenticateRequest loginUser)
        {
            try
            {
                if (loginUser.Role == "Admin")
                {
                    var users = _services.GetAllUsers();
                    if (users == null)
                    {
                        return BadRequest("User doesn't exit!! \n You need to register this user");
                    }
                    return Ok(users);
                }
                return BadRequest("You are not authorised for this request!!!");
            }
            catch (Exception ex)
            {
                Log.Information("AutoController -> GetAllUsersInfo() -> Exception => {@ex.Message}", ex.Message);
                return BadRequest("Error");
            }
        }
    }
}
