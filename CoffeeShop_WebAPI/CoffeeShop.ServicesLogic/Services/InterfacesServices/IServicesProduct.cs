namespace CoffeeShop.ServicesLogic.Services.Interfaces
{
    public interface IServicesProduct<T> where T : class
    {
        bool IsProductExistingInDb (string productName);
        Task<IEnumerable<T>> GetAllProducts();
        void UpdateProductInformation(T product);
    }
}
