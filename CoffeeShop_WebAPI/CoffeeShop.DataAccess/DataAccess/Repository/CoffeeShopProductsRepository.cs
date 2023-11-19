using CoffeeShop.DataAccess.DataAccess.DataBaseContext;
using CoffeeShop.DataAccess.DataAccess.ModelDB;
using CoffeeShop.DataAccess.DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.DataAccess.DataAccess.Repository
{
    public class CoffeeShopProductsRepository : ICoffeeShopRepository<Product>
    {
        private CoffeeShopContext _context;

        public CoffeeShopProductsRepository(CoffeeShopContext context)
        {
            _context = context;
        }
        public void Delete(Guid id)
        {
            _context.Products.Remove(GetById(id).Result);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task Update(Product item)
        {
            if (item != null)
            {
                _context.Update(item);
            }
        }

        public async Task<bool> Insert(Product item)
        {
            if (item != null)
            {
                _context.Products.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    Name = item.Name,
                    Sku = item.Sku,
                    Description = item.Description,
                    IsStock = item.IsStock,
                    Category = new Category
                    {
                        Id = Guid.NewGuid(),
                        Name = item.Category.Name
                    },
                    IdCategory = item.Category.Id,
                    Promotion = new Promotion
                    {
                        Id = Guid.NewGuid(),
                        PricePromotion = item.Promotion.PricePromotion,
                        EndDate = item.Promotion.EndDate,
                        StartDate = item.Promotion.StartDate,
                    },
                    IdPromotie = item.Promotion.Id,
                    Price = item.Price,
                    Quantity = item.Quantity,
                });
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        
    }
}
