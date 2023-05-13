using AutoMapper;
using CoffeeShop_WebApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.DataAccess.Repository;
using WebApplication1.Model;


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
        
        public CoffeeShopController( ICoffeeShopRepository<User> usersRepository, IConfiguration configuration, IMapper mapper)
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

            await _usersRepository.Insert(user);
            return Ok("Register Success");
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "Admin"),
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

        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login(UserDto request)
        {
            if (user.Email != request.Email)
            {
                return BadRequest("User not found.");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Wrong password.");
            }

             string token = CreateToken(user);

            return Ok(token);
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
