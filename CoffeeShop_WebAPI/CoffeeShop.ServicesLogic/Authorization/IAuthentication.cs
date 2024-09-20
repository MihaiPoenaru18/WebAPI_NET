using CoffeeShop_WebApi.Authorization.Models;

namespace CoffeeShop.ServicesLogic.Authorization
{
    public interface IAuthentication
    {
       Task<string> CreateToken(AuthenticateRequest request, DateTime expiresDate);
       Task<AuthenticateResponse> Authorization(AuthenticateRequest request, DateTime expiresDate);
    }
}
