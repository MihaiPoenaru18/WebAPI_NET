using CoffeeShop_WebApi.Authorization.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoffeeShop_WebApi.Authorization
{
    public class Authentication : IAuthentication
    {
        private readonly IConfiguration _configuration;

        public Authentication(IConfiguration configuration) 
        {
            _configuration = configuration;
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
            var token = CreateToken(request, expiresDate);
            if (token == null)
            {
                return null;
            }
            AuthenticateResponse authenticateResponse = new AuthenticateResponse(request, token);
            authenticateResponse.Email= request.Email;
            authenticateResponse.CreatedDate = DateTime.Now;
            authenticateResponse.ExpiresDate = expiresDate;
            return authenticateResponse;
        }
    }
}
