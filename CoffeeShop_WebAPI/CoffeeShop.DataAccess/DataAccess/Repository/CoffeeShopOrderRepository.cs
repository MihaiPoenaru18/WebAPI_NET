using CoffeeShop.DataAccess.DataAccess.DataBaseContext;
using CoffeeShop.DataAccess.DataAccess.ModelDB.Order;
using CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel;
using CoffeeShop.DataAccess.DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.DataAccess.DataAccess.Repository
{
    public class CoffeeShopOrderRepository : ICoffeeShopRepository<Order>
    {
        private CoffeeShopContext _context;
        
        public CoffeeShopOrderRepository(CoffeeShopContext context)
        {
            context = _context;
        }

        public async Task DeleteById(Guid id)
        {
            _context.Order.Remove(GetById(id).Result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _context.Order.ToListAsync();
        }

        public async Task<Order> GetById(Guid id)
        {
            return await _context.Order.FindAsync(id);
        }

        public async Task<Order> GetByName(string Name)
        {
            return await _context.Order.FindAsync(Name);
        }

        public async Task<bool> Insert(Order item)
        {
            _context.Order.Add(new Order
            {
                Id = Guid.NewGuid(),
                Address = new Address
                {
                    City = item.Address.City,
                    PostalCode = item.Address.PostalCode,
                    Country = item.Address.Country,
                    Id = Guid.NewGuid(),
                    Region = item.Address.Region
                },
                AddressId = item.Address.Id,
                Currency = item.Currency,
                TotalPrices = item.TotalPrices,
            });
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task Update(Order item)
        {
            if (item != null)
            {
                _context.Order.Update(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}
