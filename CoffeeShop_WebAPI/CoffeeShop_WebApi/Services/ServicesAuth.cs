using AutoMapper;
using CoffeeShop_WebApi.DataAccess.ModelDB;
using CoffeeShop_WebApi.Models;
using CoffeeShop_WebApi.Services.AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.DataAccess.Repository;

namespace CoffeeShop_WebApi.Services
{
    public class ServicesAuth : IServices<UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private ICoffeeShopRepository<User> _usersRepository;
        private static User user = new User();

        public ServicesAuth(ICoffeeShopRepository<User> usersRepository, IMapper mapper, IConfiguration configuration)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public UserDto GetInfo(LoginUser loginUser)
        {
            var findUser = _usersRepository.GetAll().Result.Where(x => x.Email == loginUser.Email).FirstOrDefault();
            //var s = User?.Identity?.Name;
            if (findUser == null)
            {
                return null;
            }
            var s = _mapper.Map<User, UserDto>(findUser);
            return s;
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            var mapperUser =  MapperConfig.InitializeAutomapper();
            var users = new List<UserDto>();
            foreach(var user in _usersRepository.GetAll().Result)
            {
                users.Add(mapperUser.Map<User, UserDto>(user));
            }
            return users;
        }

        public async Task<bool> IsUserRegistered(UserDto userDto)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            Guid g = Guid.NewGuid();
            user.Id = g;
            user = _mapper.Map<User>(userDto);
            user.Password = passwordHash;
            return await _usersRepository.Insert(user);
        }

        public ResposeToken CreateToken(LoginUser loginUser)
        {
            var findUser = _usersRepository.GetAll().Result.Where(x => x.Email == loginUser.Email).FirstOrDefault();
            var expiresDate = DateTime.Now.AddDays(1);
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Email, loginUser.Email),
                new Claim(ClaimTypes.Name, $"{findUser.FirstName} {findUser.LastName}"),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "User"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: expiresDate,
                    signingCredentials: creds);

            ResposeToken resposeToken = new ResposeToken();
            resposeToken.CreatedDate = DateTime.Now;
            resposeToken.ExpiresDate = expiresDate;
            resposeToken.Token = new JwtSecurityTokenHandler().WriteToken(token);

            return resposeToken;
        }


    }
}
