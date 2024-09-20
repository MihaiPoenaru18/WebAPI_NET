using CoffeeShop_WebApi.Authorization.Models;

namespace CoffeeShop.ServicesLogic.Services.Interfaces
{
    public interface IServicesNewsLetter<T> where T : class
    {
        Task<bool> GetStatusOfNewsLetter(T user);
        Task<bool> IsUserRegisteredWithNewsLetter(T user);
    }
}
