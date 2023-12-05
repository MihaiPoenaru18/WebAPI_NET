using CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel;

namespace CoffeeShop.DataAccess.DataAccess.Repository.Interfaces
{
    public interface ICoffeeShopProductsRepository<T> : ICoffeeShopRepository<T> where T : class
    {
        Task<IEnumerable<Category>> GetAllCategoris();

        Task Delete(string Name);

        Task<T> GetByName(string Name);
    }
}
