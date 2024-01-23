using CoffeeShop.ServicesLogic.EntiteModels;

namespace CoffeeShop.ServicesLogic.Services.InterfacesServices
{
    public interface IServicesOrder<T> where T : class
    {
        bool IsOrderExistInDb(Guid orderId);
        bool AddNewOrder(T order);
        T GetOrder(Guid orderId);
        bool DeleteOrder(OrderDto order);
        Task UpdateOrder(OrderDto order);
        Task<IEnumerable<T>> GetAllOrders();

    }
}
