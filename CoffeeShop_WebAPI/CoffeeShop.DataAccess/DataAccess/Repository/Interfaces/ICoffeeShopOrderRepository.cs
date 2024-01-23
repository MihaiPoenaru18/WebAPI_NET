using CoffeeShop.DataAccess.DataAccess.ModelDB.OrderModels;

namespace CoffeeShop.DataAccess.DataAccess.Repository.Interfaces
{
    public interface ICoffeeShopOrderRepository<T> : ICoffeeShopRepository<T> where T : class
    {
        
    }
}
