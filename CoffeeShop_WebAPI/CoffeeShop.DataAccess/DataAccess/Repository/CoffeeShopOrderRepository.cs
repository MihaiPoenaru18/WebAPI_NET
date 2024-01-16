using CoffeeShop.DataAccess.DataAccess.DataBaseContext;
using CoffeeShop.DataAccess.DataAccess.ModelDB.Order;
using CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel;
using CoffeeShop.DataAccess.DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.DataAccess.DataAccess.Repository
{
    public class CoffeeShopOrderRepository : ICoffeeShopOrderRepository<Order>
    {
        private CoffeeShopContext _context;
        
        public CoffeeShopOrderRepository(CoffeeShopContext context)
        {
            _context = context;
        }

        public async Task DeleteById(Guid id)
        {
            _context.Order.Remove(GetById(id).Result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _context.Order
                .Include(p=>p.Products)
                   .ThenInclude(c=>c.Category)
                .Include(p => p.Products)
                   .ThenInclude(p => p.Promotion)
                .Include(a=>a.Address)
                .ToListAsync();
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
                OrderId = Guid.NewGuid(),
                Currency = item.Currency,
                TotalPrices = item.Products.Sum(x => x.Price),
                Status = OrderStatus.Processing,
                Address = new Address
                {
                    City = item.Address.City,
                    PostalCode = item.Address.PostalCode,
                    Country = item.Address.Country,
                    Street = item.Address.Street,
                    Id = Guid.NewGuid(),
                    Region = item.Address.Region
                },
                Products = item.Products.Where(p => !_context.Products.Any(existingProduct =>  existingProduct.Name == p.Name))
                                        .Select(p => new Product
                                        {
                                            Id = Guid.NewGuid(),
                                            Name = p.Name,
                                            Sku = p.Sku,
                                            Description = p.Description,
                                            IsStock = p.IsStock,
                                            Currency = p.Currency,
                                            Promotion = p.Promotion,
                                            Category = p.Category,
                                            Price = p.Price,
                                            Quantity = p.Quantity,
                                            
                                        })
                                        .ToList(),
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

        public async Task UpdateStatus(Guid id,OrderStatus status)
        {
            var orderToUpdate = await GetById(id);
            if (orderToUpdate != null)
            {
                orderToUpdate.Status = status;
                await Update(orderToUpdate);
            }
        }
    }
}
