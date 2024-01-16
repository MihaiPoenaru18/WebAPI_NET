using CoffeeShop.DataAccess.DataAccess.ModelDB.Order;

namespace CoffeeShop.DataAccess.DataAccess.Repository.Interfaces
{
    public interface ICoffeeShopOrderRepository<T> : ICoffeeShopRepository<T> where T : class
    {
        Task UpdateStatus(,Guid Id,OrderStatus status);
    }
}
