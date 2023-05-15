using AutoMapper;
using CoffeeShop_WebApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.DataAccess.Repository;
using WebApplication1.Models;

namespace CoffeeShop_WebApi.Services
{
    public class Services : IServices<UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private ICoffeeShopRepository<User> _usersRepository;
        private static User user = new User();

        public Services(ICoffeeShopRepository<User> usersRepository, IMapper mapper, IConfiguration configuration)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            _configuration = configuration;
        }
        public UserDto GetInfo(LoginUser loginUser)// gandeste la un parametru generic)
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

        public async Task<bool> IsUserRegistered(UserDto userDto)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            Guid g = Guid.NewGuid();
            user.Id = g;
            user = _mapper.Map<User>(userDto);
            user.PasswordHash = passwordHash;
            return await _usersRepository.Insert(user);
        }


        public string CreateToken(LoginUser loginUser)
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
    }
}
