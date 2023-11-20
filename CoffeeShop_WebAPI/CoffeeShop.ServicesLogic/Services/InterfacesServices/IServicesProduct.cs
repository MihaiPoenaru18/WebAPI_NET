namespace CoffeeShop.ServicesLogic.Services.Interfaces
{
    public interface IServicesProduct<T> where T : class
    {
        Task<bool> IsProductExistingInDb (string productName);
        Task<IEnumerable<T>> GetAllProducts();
        Task AddNewProductsInDBAsync(List<T> products); 
        void UpdateProductInformation(T product);
    }
}
