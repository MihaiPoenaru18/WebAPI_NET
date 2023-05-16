using CoffeeShop_WebApi.Models;

namespace CoffeeShop_WebApi.Services
{
    public interface IServices<T> where T : class
    {
        T GetInfo(LoginUser loginUse);
        IEnumerable<T> GetAllUsers();
        Task<bool> IsUserRegistered(T user);
        ResposeToken CreateToken(LoginUser loginUser);
    }
}
