using CoffeeShop.DataAccess.DataAccess.DataBaseContext;
using CoffeeShop.DataAccess.DataAccess.ModelDB.User;
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
            return await _context.Users.ToListAsync();
        }

        public async Task<bool> Insert(User user)
        {
            if (user != null && !IsUserExistingInDB(user))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.Id = Guid.NewGuid();
                _context.Users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    IdUserNewsLetter = Guid.NewGuid(),
                    Password = user.Password,
                    Role = user.Role,
                    UserWithNewsLetter = new UserWithNewsLetter
                    {
                        Email = user.Email,
                        Id = Guid.NewGuid(),
                        IsNewsLetterActive = user.UserWithNewsLetter.IsNewsLetterActive,
                        Name = user.FirstName + user.LastName
                    }
                });
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task Delete(string Name)
        {
            var user = GetById(GetByName(Name).Result.Id).Result;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetByName(string Name)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                return await _context.Users.FindAsync(Name);
            }
            return null;
        }

        public bool IsUserExistingInDB(User user)
        {
            var userFromDb = GetAll().Result.Where(x => x.Email == user.Email && BCrypt.Net.BCrypt.Verify(user.Password, x.Password)).FirstOrDefault();
            try
            {
                if (userFromDb != null)
                {
                    user.FirstName = userFromDb.FirstName;
                    user.LastName = userFromDb.LastName;
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Outer Exception: {ex.Message}");
            }
            return false;
        }

        public async Task<User> GetById(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task Update(User item)
        {
            if (item != null)
            {
                _context.Users.Update(item);
                await _context.SaveChangesAsync();
            }
        }

        public string GetNameByEmail(string email)
        {
            var name = _context.Users.Where(e => e.Email == email).FirstOrDefault().FirstName;
            return name;
        }

       
    }
}
