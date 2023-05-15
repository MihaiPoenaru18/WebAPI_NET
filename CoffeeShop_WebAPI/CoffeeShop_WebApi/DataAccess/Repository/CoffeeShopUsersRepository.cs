using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

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
           if(user!= null && !IsUserExistingInDB(user))
            {
                _context.User.Add(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public void Delete(Guid id)
        {
            var user = GetById(id).Result;
            _context.User.Remove(user);
        }

        public bool IsUserExistingInDB(User user)
        {
            var email = GetAll().Result.Where(x=>x.Email == user.Email).FirstOrDefault();
            if(email != null)
            {
                return true;
            }
            return false;
        }

        public async Task<User> GetById(Guid id)
        {
           return await _context.User.FindAsync(id);
        }

        public Task Update(User item)
        {
            throw new NotImplementedException();
        }
    }
}
