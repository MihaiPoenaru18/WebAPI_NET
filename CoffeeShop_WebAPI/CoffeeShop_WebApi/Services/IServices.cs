using CoffeeShop_WebApi.Models;

namespace CoffeeShop_WebApi.Services
{
    public interface IServices<T> where T : class
    {
        T GetInfo(LoginUser loginUse);
         Task<bool> IsUserRegistered(T user);
        string CreateToken(LoginUser loginUser);
    }
}
