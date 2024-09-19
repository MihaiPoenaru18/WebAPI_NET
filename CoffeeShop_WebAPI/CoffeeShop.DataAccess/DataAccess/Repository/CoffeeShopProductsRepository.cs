using CoffeeShop.DataAccess.DataAccess.DataBaseContext;
using CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel;
using CoffeeShop.DataAccess.DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

public class CoffeeShopProductsRepository : ICoffeeShopProductsRepository<Product>
{
    private readonly CoffeeShopContext _context;

    public CoffeeShopProductsRepository(CoffeeShopContext context)
    {
        _context = context;
    }

    // Improved Delete methods
    public async Task Delete(string name)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteById(Guid id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }

    // Improved GetByName method
    public async Task<Product> GetByName(string name)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
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

    // Improved AddCategory method
    public async Task<Category> AddCategory(Category category)
    {
        var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Name == category.Name);

        if (existingCategory == null && existingCategory.Name == null && existingCategory.ImagePath ==null)
        {
            existingCategory = new Category
            {
                Name = category.Name,
                ImagePath = category.ImagePath,
            };

            _context.Categories.Add(existingCategory);
            await _context.SaveChangesAsync();
        }
        return existingCategory;
    }
    public async Task<IEnumerable<Category>> GetAllCategories()
    {
        return await _context.Categories.ToListAsync();
    }


    // Improved AddPromotion method
    public async Task<Promotion> AddPromotion(Promotion promotion)
    {
        var existingPromotion = await _context.Promotion.FirstOrDefaultAsync(p =>
            p.PricePromotion == promotion.PricePromotion &&
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
        }
        return existingPromotion;
    }

    // Improved Insert method
    public async Task<bool> Insert(Product item)
    {
        try
        {
            var existingCategory = await AddCategory(item.Category);
            var existingPromotion = await AddPromotion(item.Promotion);

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
                ImagePath = item.ImagePath,
            };

            _context.Products.Add(newProduct);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }

    }
}