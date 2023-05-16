using CoffeeShop_WebApi.DataAccess.ModelDB;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.DataAccess
{
    public class CoffeeShopContext : DbContext
    {
        public CoffeeShopContext(DbContextOptions<CoffeeShopContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
       
    }
}
