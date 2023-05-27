using BCrypt.Net;
using CoffeeShop.DataAccess.DataAccess.DataBaseContext;
using CoffeeShop_WebApi.DataAccess.ModelDB;
using Microsoft.EntityFrameworkCore;

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
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
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
            var userFromDb = GetAll().Result.Where(x=>x.Email == user.Email && BCrypt.Net.BCrypt.Verify(user.Password,x.Password)).FirstOrDefault();
            if(userFromDb != null)
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
