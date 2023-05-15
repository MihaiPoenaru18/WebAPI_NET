using AutoMapper;
using Azure.Core;
using CoffeeShop_WebApi.Models;
using CoffeeShop_WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.DataAccess.Repository;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeShopController : ControllerBase
    {
       
        private ICoffeeShopRepository<User> _usersRepository;
        private IServices<UserDto> _services;
        

        public CoffeeShopController(ICoffeeShopRepository<User> usersRepository, IServices<UserDto> services)
        {
            _services= services;
            _usersRepository = usersRepository;
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

        [HttpPost("Login")]
        public ActionResult<User> Login(LoginUser loginUser)
        {
            var findUser = _usersRepository.GetAll().Result.Where(x => x.Email == loginUser.Email).FirstOrDefault();
            if (findUser == null)
            {
                return BadRequest("User not found.");
            }

            if (!BCrypt.Net.BCrypt.Verify(loginUser.Password, findUser.PasswordHash))
            {
                return BadRequest("Wrong password.");
            }

            string token = _services.CreateToken(loginUser);

            return Ok(token);
        }

        [HttpGet("GetUserInfo"),Authorize]
        public ActionResult<UserDto> GetUserInfo([FromQuery] LoginUser loginUser)
        {
           //_services.GetInfo(loginUser);
            var userName = User?.Identity?.Name;
            var roleClaims = User?.FindAll(ClaimTypes.Role);
            var roles = roleClaims?.Select(c => c.Value).ToList();
            var roles2 = User?.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();
           // return Ok(new { userName, roles, roles2 });

            //var s = _mapper.Map<User,UserDto>(findUser);
            return Ok( _services.GetInfo(loginUser));
        }

        [HttpGet("GetAllUsersInfo"), Authorize]
        public ActionResult<IEnumerable<User>> GetAllUsersInfoo([FromQuery] LoginUser loginUser)
        {
            if (loginUser.Role == "Admin")
            {
                var allUsers = _usersRepository.GetAll().Result;
                if (allUsers == null)
                {
                    return BadRequest("Database is empty!!!");
                }

                return allUsers.OrderBy(x => x.FirstName).ToList();
            }
            return BadRequest("You are not authorised for this request!!!");
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            if (user == null)
            {
                return BadRequest("The body is emplty");
            }
            await _usersRepository.Insert(user);
            return Ok("Register Success");
        }
    }
}
