using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

namespace WebApplication1.DataAccess
{
    public class CoffeeShopContext : DbContext
    {
        public CoffeeShopContext(DbContextOptions<CoffeeShopContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
       
    }
}
