using CoffeeShop_WebApi.Authorization.Models;

namespace CoffeeShop_WebApi.Services
{
    public interface IServices<T> where T : class
    {
        T GetInfo(AuthenticateRequest loginUse);

        IEnumerable<T> GetAllUsers();

        Task<bool> IsUserRegistered(T user);

        AuthenticateResponse? Authenticate(AuthenticateRequest request);
    }
}
