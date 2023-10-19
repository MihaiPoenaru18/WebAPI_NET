using CoffeeShop.DataAccess.DataAccess.DataBaseContext;
using CoffeeShop.DataAccess.DataAccess.ModelDB;
using CoffeeShop_WebApi.DataAccess.ModelDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            return await _context.News.ToListAsync();
        }

        public void Delete(Guid id)
        {
            var user = GetById(id).Result;
            _context.News.Remove(user);
        }

        public async Task<UserWithNewsLetter> GetById(Guid id)
        {
            return await _context.News.FindAsync(id);
        }

        public async Task<bool> Insert(UserWithNewsLetter userWithNews)
        {
            if (!IsUserExistingInDB(userWithNews) && userWithNews != null)
            {
                _context.Add(userWithNews);
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
                _context.News.Update(item);
            }
        }
        
    }
}
