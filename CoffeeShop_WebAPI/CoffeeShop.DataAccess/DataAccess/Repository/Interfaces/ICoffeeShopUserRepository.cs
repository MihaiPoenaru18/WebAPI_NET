namespace CoffeeShop.DataAccess.DataAccess.Repository.Interfaces
{
    public interface ICoffeeShopUserRepository<T> : ICoffeeShopRepository<T> where T : class
    {
        Task<bool> IsUserExistingInDB(T item);

        Task<string> GetUserByEmail(string email);
    }
}
