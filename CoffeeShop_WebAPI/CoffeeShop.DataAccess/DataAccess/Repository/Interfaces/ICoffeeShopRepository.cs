namespace CoffeeShop.DataAccess.DataAccess.Repository.Interfaces
{
    public interface ICoffeeShopRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(Guid id);

        Task<T> GetByName(string Name);

        Task<bool> Insert(T item);

        Task Update(T item);

        Task Delete(string Name);

    }
}
