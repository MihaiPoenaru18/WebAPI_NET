namespace CoffeeShop.DataAccess.DataAccess.Repository.Interfaces
{
    public interface ICoffeeShopUserRepository<T> : ICoffeeShopRepository<T> where T : class
    {
        bool IsUserExistingInDB(T item);

        string GetNameByEmail(string email);
    }
}
