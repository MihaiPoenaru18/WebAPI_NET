using CoffeeShop.ServicesLogic.EntiteModels;

namespace CoffeeShop.ServicesLogic.Services.InterfacesServices
{
    public interface IServicesOrder<T> where T : class
    {
        Task<bool> IsOrderExistInDb(Guid orderId);
        Task<bool> AddNewOrder(T order);
        Task<T> GetOrder(Guid orderId);
        Task<bool> DeleteOrder(Guid orderId);
        Task UpdateOrder(T order);
        Task<IEnumerable<T>> GetAllOrders();
    }
}
