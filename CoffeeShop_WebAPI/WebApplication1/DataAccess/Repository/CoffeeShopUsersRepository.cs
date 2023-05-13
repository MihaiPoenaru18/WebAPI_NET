using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

namespace WebApplication1.DataAccess.Repository
{
    public class CoffeeShopUserRepository : ICoffeeShopRepository<User>
    {
        private readonly CoffeeShopContext _context;
        public CoffeeShopUserRepository(CoffeeShopContext context) 
        {
            _context = context; 
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<bool> Insert(User user)
        {
           if(user != null)
            {
               //check if user exist in db
                _context.User.Add(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Update(User item)
        {
            throw new NotImplementedException();
        }
    }
}
