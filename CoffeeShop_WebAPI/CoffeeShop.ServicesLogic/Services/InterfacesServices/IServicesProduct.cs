using CoffeeShop.ServicesLogic.EntiteModels.ModelsForProducts;

namespace CoffeeShop.ServicesLogic.Services.Interfaces
{
    public interface IServicesProduct<T> where T : class
    {
        Task<bool> IsProductExistingInDb (string productName);
        Task<IEnumerable<T>> GetAllProducts();
        Task<IEnumerable<CategoryDto>> GetAllCategories ();
        Task<bool> AddNewProducts(List<T> products); 
        Task UpdateProductInformation(T product);
        Task<bool> DeleteProducts(List<T> product);

    }
}
