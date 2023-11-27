using CoffeeShop.DataAccess.DataAccess.DataBaseContext;
using CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel;
using CoffeeShop.DataAccess.DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.DataAccess.DataAccess.Repository
{
    public class CoffeeShopProductsRepository : ICoffeeShopProductsRepository<Product>
    {
        private CoffeeShopContext _context;

        public CoffeeShopProductsRepository(CoffeeShopContext context)
        {
            _context = context;
        }
        public async Task Delete(string Name)
        {
            _context.Products.Remove(GetById(GetByName(Name).Result.Id).Result);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetByName(string Name)
        {
            return await _context.Products.FindAsync(Name);
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task Update(Product item)
        {
            if (item != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> Insert(Product item)
        {
            var existingCategory = _context.Categories.FirstOrDefault(c => c.Id == item.Category.Id);

            // If the category doesn't exist, create a new one
            if (existingCategory == null)
            {
                existingCategory = new Category
                {
                    Id = item.Category.Id,
                    Name = item.Category.Name
                };

                _context.Categories.Add(existingCategory);
            }

            _context.Products.Add(new Product
            {
                Id = Guid.NewGuid(),
                Name = item.Name,
                Sku = item.Sku,
                Description = item.Description,
                IsStock = item.IsStock,
                Currency = item.Currency,
                Category = existingCategory,  // Use the existing or newly created category
                IdCategory = existingCategory.Id,
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

        public async Task<IEnumerable<Category>> GetAllCategoris()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
