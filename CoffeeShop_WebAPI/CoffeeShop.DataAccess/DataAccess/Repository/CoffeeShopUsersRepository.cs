using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeShop.DataAccess.DataAccess.DataBaseContext;
using CoffeeShop.DataAccess.DataAccess.ModelDB.UserModels;
using CoffeeShop.DataAccess.DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.DataAccess.Repository
{
    public class CoffeeShopUserRepository : ICoffeeShopUserRepository<User>
    {
        private readonly CoffeeShopContext _context;

        public CoffeeShopUserRepository(CoffeeShopContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.Include(n => n.UserWithNewsLetter).ToListAsync();
        }

        public async Task<User> GetById(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> Insert(User user)
        {
            if (user == null || await IsUserExistingInDB(user))
            {
                return false;
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.UserId = Guid.NewGuid();
            user.IdUserNewsLetter = Guid.NewGuid();

            var newUser = new User
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                IdUserNewsLetter = user.IdUserNewsLetter,
                Password = user.Password,
                Role = user.Role,
                UserWithNewsLetter = new UserWithNewsLetter
                {
                    Email = user.Email,
                    Id = Guid.NewGuid(),
                    IsNewsLetterActive = user.UserWithNewsLetter?.IsNewsLetterActive ?? false,
                    Name = $"{user.FirstName} {user.LastName}"
                }
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task Update(User item)
        {
            if (item != null)
            {
                _context.Users.Update(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteById(Guid id)
        {
            var user = await GetById(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsUserExistingInDB(User user)
        {
            var userFromDb = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == user.Email);

            if (userFromDb != null && BCrypt.Net.BCrypt.Verify(user.Password, userFromDb.Password))
            {
                CopyUserProperties(userFromDb, user);
                return true;
            }

            return false;
        }

        public async Task<string> GetUserByEmail(string email)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(e => e.Email == email);
            return user?.FirstName;
        }

        private void CopyUserProperties(User source, User destination)
        {
            destination.FirstName = source.FirstName;
            destination.LastName = source.LastName;
            destination.Email = source.Email;
            destination.Role = source.Role;
            destination.IdUserNewsLetter = source.IdUserNewsLetter;
            destination.UserId = source.UserId;
            destination.UserWithNewsLetter = source.UserWithNewsLetter;
        }
    }
}