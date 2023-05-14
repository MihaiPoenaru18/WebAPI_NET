namespace WebApplication1.DataAccess.Repository
{
    public interface ICoffeeShopRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(Guid id);

        Task<bool>Insert(T item);

        Task Update(T item);

        void Delete(Guid id);
    }
}
