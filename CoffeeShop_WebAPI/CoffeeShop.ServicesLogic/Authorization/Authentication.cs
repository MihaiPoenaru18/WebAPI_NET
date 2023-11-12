using CoffeeShop_WebApi.Authorization.Models;
using CoffeeShop_WebApi.DataAccess.ModelDB;
using CoffeeShop_WebApi.Services.AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.DataAccess.Repository;

namespace CoffeeShop.ServicesLogic.Authorization
{
    public class Authentication : IAuthentication
    {
        private readonly IConfiguration _configuration;
        private readonly ICoffeeShopRepository<User> _usersRepository;

        public Authentication(IConfiguration configuration, ICoffeeShopRepository<User> _usersRepository) 
        {
            _configuration = configuration;
            this._usersRepository = _usersRepository;
        }

        public string CreateToken(AuthenticateRequest request, DateTime expiresDate)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "User"),
            };
            
            var key = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: expiresDate,
                    signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public AuthenticateResponse Authorization(AuthenticateRequest request, DateTime expiresDate)
        {
            var user = MapperConfig<AuthenticateRequest, User>.InitializeAutomapper().Map<AuthenticateRequest, User>(request);
            if (_usersRepository.IsUserExistingInDB(user))
            {
                var token = CreateToken(request, expiresDate);
                if (token == null)
                {
                    return null;
                }
                AuthenticateResponse authenticateResponse = new AuthenticateResponse(request, token);
                authenticateResponse.Email = request.Email;
                authenticateResponse.CreatedDate = DateTime.Now;
                authenticateResponse.ExpiresDate = expiresDate;
                authenticateResponse.Name = _usersRepository.GetNameByEmail(request.Email);
                return authenticateResponse;
            }
            return null;
        }
    }
}
