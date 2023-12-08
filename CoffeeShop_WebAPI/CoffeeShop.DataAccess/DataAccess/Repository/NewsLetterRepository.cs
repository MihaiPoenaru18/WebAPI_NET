using CoffeeShop.DataAccess.DataAccess.DataBaseContext;
using CoffeeShop.DataAccess.DataAccess.ModelDB.User;
using CoffeeShop.DataAccess.DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.DataAccess.DataAccess.Repository
{
    public class NewsLetterRepository : ICoffeeShopUserRepository<UserWithNewsLetter>
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

        public async Task<UserWithNewsLetter> GetByName(string Name)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                return await _context.Newsletters.FindAsync(Name);
            }
            return null;
        }

        public async Task Delete(string Name)
        {
            var user = GetById(GetByName(Name).Result.Id).Result;
            _context.Newsletters.Remove(user);
            await _context.SaveChangesAsync();
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

        public string GetNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Task DeleteById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
