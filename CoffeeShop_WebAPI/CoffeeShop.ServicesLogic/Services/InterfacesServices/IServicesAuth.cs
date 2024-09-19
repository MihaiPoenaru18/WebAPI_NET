using CoffeeShop_WebApi.Authorization.Models;

namespace CoffeeShop.ServicesLogic.Services.Interfaces
{
    public interface IServicesAuth<T> where T : class
    {
        Task<T> GetInfo(AuthenticateRequest loginUse);

        Task<IEnumerable<T>> GetAllUsers();

        Task<bool> IsUserRegistered(T user);

        Task<AuthenticateResponse?> Authenticate(AuthenticateRequest request);
    }
}
