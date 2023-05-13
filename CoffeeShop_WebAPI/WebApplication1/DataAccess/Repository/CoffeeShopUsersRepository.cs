using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

namespace WebApplication1.DataAccess.Repository
{
    public class CoffeeShopUsersRepository : ICoffeeShopRepository<Users>
    {
        private readonly CoffeeShopContext _context;
        public CoffeeShopUsersRepository(CoffeeShopContext context) 
        {
            _context = context; 
        }

        public async Task<IEnumerable<Users>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public Task<Users> Insert(Users item)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Users> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Users item)
        {
            throw new NotImplementedException();
        }
    }
}
