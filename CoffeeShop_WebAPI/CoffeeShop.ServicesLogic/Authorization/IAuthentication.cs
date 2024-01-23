using CoffeeShop_WebApi.Authorization.Models;

namespace CoffeeShop.ServicesLogic.Authorization
{
    public interface IAuthentication
    {
        string CreateToken(AuthenticateRequest request, DateTime expiresDate);
        AuthenticateResponse Authorization(AuthenticateRequest request, DateTime expiresDate);
    }
}
