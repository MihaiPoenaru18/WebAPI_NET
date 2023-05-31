using CoffeeShop_WebApi.Authorization.Models;
namespace CoffeeShop_WebApi.Authorization
{
    public interface IAuthentication
    {
        string CreateToken(AuthenticateRequest request, DateTime expiresDate);
        AuthenticateResponse Authorization(AuthenticateRequest request, DateTime expiresDate);
    }
}
