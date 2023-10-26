using CoffeeShop.DataAccess.DataAccess.ModelDB;
using CoffeeShop_WebApi.DataAccess.ModelDB;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.DataAccess.DataAccess.DataBaseContext
{
    public class CoffeeShopContext : DbContext
    {
        public CoffeeShopContext(DbContextOptions<CoffeeShopContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<UserWithNewsLetter> News { get; set; }
    }
}
