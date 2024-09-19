using CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel;

namespace CoffeeShop.DataAccess.DataAccess.Repository.Interfaces
{
    public interface ICoffeeShopProductsRepository<T> : ICoffeeShopRepository<T> where T : class
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> AddCategory(Category category);
        Task<Promotion> AddPromotion(Promotion promotion);
        Task Delete(string name);
        Task<T> GetByName(string name);
    }
}
