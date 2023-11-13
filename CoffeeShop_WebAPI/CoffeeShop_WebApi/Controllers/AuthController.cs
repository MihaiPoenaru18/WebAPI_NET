using CoffeeShop.ServicesLogic.Authorization;
using CoffeeShop_WebApi.Authorization.Models;
using CoffeeShop_WebApi.DataAccess.ModelDB;
using CoffeeShop.ServicesLogic.EntiteModels;
using CoffeeShop.ServicesLogic.Services;
using Microsoft.AspNetCore.Mvc;
using CoffeeShop_WebApi.Controllers;

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
                return Ok(new ApiResponse { Success = true, Message = "Register Success" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                return BadRequest(ex.Message);
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
                return BadRequest(ex.Message);
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
                    if (_services.GetAllUsers() == null)
                    {
                        return BadRequest("User doesn't exit!! \n You need to register this user");
                    }
                    return Ok(_services.GetAllUsers());
                }
                return BadRequest("You are not authorised for this request!!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
