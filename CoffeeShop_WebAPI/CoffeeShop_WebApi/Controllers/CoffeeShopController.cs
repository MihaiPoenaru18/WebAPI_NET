using AutoMapper;
using Azure.Core;
using CoffeeShop_WebApi.Models;
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
        public static User user = new User();
        private ICoffeeShopRepository<User> _usersRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public CoffeeShopController(ICoffeeShopRepository<User> usersRepository, IConfiguration configuration, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            if (request == null)
            {
                return BadRequest("The fiels are emplty!!!");
            }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            Guid g = Guid.NewGuid();
            user.Id = g;
            user = _mapper.Map<User>(request);
            user.PasswordHash = passwordHash;

            bool isUserExistingInDB = await _usersRepository.Insert(user);
            if (!isUserExistingInDB)
            {
                return BadRequest("The user alright exist!!!");
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

            string token = CreateToken(loginUser);

            return Ok(token);
        }

        [HttpGet("GetUserInfo"),Authorize]
        public ActionResult<UserDto> GetUserInfo([FromQuery] LoginUser loginUser)
        {
            var findUser = _usersRepository.GetAll().Result.Where(x => x.Email == loginUser.Email).FirstOrDefault();
            //var s = User?.Identity?.Name;
            if (findUser == null)
            {
                return BadRequest("findUser == null");
            }
            var userName = User?.Identity?.Name;
            var roleClaims = User?.FindAll(ClaimTypes.Role);
            var roles = roleClaims?.Select(c => c.Value).ToList();
            var roles2 = User?.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();
            return Ok(new { userName, roles, roles2 });

            //var s = _mapper.Map<User,UserDto>(findUser);
            //return Ok(s) ;
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

        private string CreateToken(LoginUser loginUser)
        {
            var findUser = _usersRepository.GetAll().Result.Where(x => x.Email == loginUser.Email).FirstOrDefault();
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Email, loginUser.Email),
                new Claim(ClaimTypes.Name,$"{findUser.FirstName} {findUser.LastName}"),
                new Claim(ClaimTypes.Role, "User"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
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
