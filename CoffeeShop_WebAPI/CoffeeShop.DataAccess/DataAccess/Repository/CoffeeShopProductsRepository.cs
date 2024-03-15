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
        public async Task DeleteById(Guid id)
        {
            _context.Products.Remove(GetById(id).Result);
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
            return await _context.Products
                                          .Include(p => p.Category)
                                          .Include(p => p.Promotion)
                                          .ToListAsync();
        }

        public async Task Update(Product item)
        {
            if (item != null)
            {
                _context.Products.Update(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Category> AddCategory(Category category)
        {
            var existingCategory = _context.Categories.FirstOrDefault(c => c.Name == category.Name);

            if (existingCategory.Name == null && existingCategory.ImagePath ==null )
            {
                existingCategory = new Category
                {
                    Name = category.Name,
                    ImagePath = category.ImagePath,
                };

                _context.Categories.Add(existingCategory);
                await _context.SaveChangesAsync();
                return existingCategory;
            }
            return existingCategory;
        }

        public async Task<Promotion> AddPromotion(Promotion promotion)
        {
            var existingPromotion = _context.Promotion.FirstOrDefault(p =>p.PricePromotion == promotion.PricePromotion &&
                                                                          p.StartDate == promotion.StartDate &&
                                                                          p.EndDate == promotion.EndDate);
            if (existingPromotion == null)
            {
                existingPromotion = new Promotion
                {
                    PricePromotion = promotion.PricePromotion,
                    StartDate = promotion.StartDate,
                    EndDate = promotion.EndDate
                };

                _context.Promotion.Add(existingPromotion);
                await _context.SaveChangesAsync();
                return existingPromotion;
            }
            return existingPromotion;
        }

        public async Task<bool> Insert(Product item)
        {
            var existingCategory = await AddCategory(item.Category);
            var existingPromotion = await AddPromotion(item.Promotion);

            // Use the existing product's properties
            var newProduct = new Product
            {
                Id = Guid.NewGuid(),
                Name = item.Name,
                Sku = item.Sku,
                Description = item.Description,
                IsStock = item.IsStock,
                Currency = item.Currency,
                CategoryId = existingCategory.Id,
                PromotionId = existingPromotion.Id,
                Price = item.Price,
                Quantity = item.Quantity,
            };

            _context.Products.Add(newProduct);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Category>> GetAllCategoris()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
