using AutoMapper;
using Azure.Core;
using CoffeeShop_WebApi.DataAccess.ModelDB;
using CoffeeShop_WebApi.Models;
using CoffeeShop_WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpGet("GetToken")]
        public ActionResult<ResposeToken> Login([FromQuery] LoginUser loginUser)
        {
            var findUser = _services.GetAllUsers().Where(x => x.Email == loginUser.Email).FirstOrDefault();
            if (findUser == null)
            {
                return BadRequest("User not found.");
            }

            if (!BCrypt.Net.BCrypt.Verify(loginUser.Password, findUser.Password))
            {
                return BadRequest("Wrong password.");
            }

            return Ok(_services.CreateToken(loginUser));
        }

        [HttpGet("GetUserInfo") ,]
        public ActionResult<UserDto> GetUserInfo([FromQuery] LoginUser loginUser)
        {
            //_services.GetInfo(loginUser);
            //var userName = User?.Identity?.Name;
            //var roleClaims = User?.FindAll(ClaimTypes.Role);
            //var roles = roleClaims?.Select(c => c.Value).ToList();
            //var roles2 = User?.Claims
            //    .Where(c => c.Type == ClaimTypes.Role)
            //    .Select(c => c.Value)
            //    .ToList();
            //return Ok(new { userName, roles, roles2 });
            //var s = _mapper.Map<User,UserDto>(findUser);
            return Ok(_services.GetInfo(loginUser));
        }

        [HttpGet("GetAllUsersInfo"), Authorize]
        public ActionResult<IEnumerable<User>> GetAllUsersInfoo([FromQuery] LoginUser loginUser)
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
