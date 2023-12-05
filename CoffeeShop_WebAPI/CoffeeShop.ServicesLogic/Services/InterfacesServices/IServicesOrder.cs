using CoffeeShop.ServicesLogic.EntiteModels;

namespace CoffeeShop.ServicesLogic.Services.InterfacesServices
{
    public interface IServicesOrder<T> where T : class
    {
        Task<bool> IsOrderExistInDb(Guid orderId);
        Task<bool> AddNewOrder(T order);
        Task<T> GetOrder(Guid orderId);
        Task<bool> DeleteOrder(OrderDto order);
        Task UpdateOrder(OrderDto order);
        Task<IEnumerable<T>> GetAllOrders();

    }
}
