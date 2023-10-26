using CoffeeShop.DataAccess.DataAccess.DataBaseContext;
using CoffeeShop.DataAccess.DataAccess.ModelDB;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DataAccess.Repository;

namespace CoffeeShop.DataAccess.DataAccess.Repository
{
    public class NewsLetterRepository : ICoffeeShopRepository<UserWithNewsLetter>
    {
        private readonly CoffeeShopContext _context;
        public NewsLetterRepository(CoffeeShopContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserWithNewsLetter>> GetAll()
        {
            return await _context.Newsletters.ToListAsync();
        }

        public void Delete(Guid id)
        {
            var user = GetById(id).Result;
            _context.Newsletters.Remove(user);
        }

        public async Task<UserWithNewsLetter> GetById(Guid id)
        {
            return await _context.Newsletters.FindAsync(id);
        }

        public async Task<bool> Insert(UserWithNewsLetter userWithNews)
        {
            if (!IsUserExistingInDB(userWithNews) && userWithNews != null)
            {    
                _context.Add(new UserWithNewsLetter
                {
                    Id = Guid.NewGuid(),
                    Email = userWithNews.Email,
                    IsNewsLetterActive = userWithNews.IsNewsLetterActive,
                    Name = userWithNews.Name, 
                });
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public bool IsUserExistingInDB(UserWithNewsLetter userWithNews) 
        { 
            var userWithNewsLetterDB = GetAll().Result.Where(x => x.Email.Equals(userWithNews.Email)).FirstOrDefault();
            if (userWithNewsLetterDB != null)
            {
                userWithNews.Email = userWithNewsLetterDB.Email;
                userWithNews.IsNewsLetterActive = true;
                return true;
            }
            return false;
        }

        public async Task Update(UserWithNewsLetter item)
        {
            if (item != null)
            {
                _context.Newsletters.Update(item);
            }
        }
        
    }
}
