using CoffeeShop.DataAccess.DataAccess.ModelDB.Order;
using CoffeeShop.DataAccess.DataAccess.Repository.Interfaces;

namespace CoffeeShop.DataAccess.DataAccess.Repository
{
    public class CoffeeShopOrderRepository : ICoffeeShopRepository<Order>
    {
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Insert(Order item)
        {
            throw new NotImplementedException();
        }

        public Task Update(Order item)
        {
            throw new NotImplementedException();
        }
    }
}
