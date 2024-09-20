
using AutoMapper;
using CoffeeShop.DataAccess.DataAccess.ModelDB.UserModels;
using CoffeeShop.DataAccess.DataAccess.Repository.Interfaces;
using CoffeeShop_WebApi.Authorization.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoffeeShop.ServicesLogic.Authorization
{
    public class Authentication : IAuthentication
    {
        private readonly IConfiguration _configuration;
        private readonly ICoffeeShopUserRepository<User> _usersRepository;
        private readonly IMapper _mapper;

        public Authentication(IConfiguration configuration, ICoffeeShopUserRepository<User> _usersRepository, IMapper mapper) 
        {
            _configuration = configuration;
            this._usersRepository = _usersRepository;
            _mapper = mapper;
        }

        public async Task<string> CreateToken(AuthenticateRequest request, DateTime expiresDate)
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

        public async Task<AuthenticateResponse> Authorization(AuthenticateRequest request, DateTime expiresDate)
        {
            var user = _mapper.Map<User>(request);
            if (await _usersRepository.IsUserExistingInDB(user))
            {
                var token = CreateToken(request, expiresDate);
                if (token == null)
                {
                    return null;
                }
                AuthenticateResponse authenticateResponse = new AuthenticateResponse(request, await token);
                authenticateResponse.Email = request.Email;
                authenticateResponse.CreatedDate = DateTime.Now;
                authenticateResponse.ExpiresDate = expiresDate;
                authenticateResponse.Name =await _usersRepository.GetUserByEmail(request.Email);
                return authenticateResponse;
            }
            return null;
        }
    }
}
