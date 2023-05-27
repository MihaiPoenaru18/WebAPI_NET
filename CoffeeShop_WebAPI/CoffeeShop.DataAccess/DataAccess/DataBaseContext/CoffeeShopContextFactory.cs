using CoffeeShop.DataAccess.DataAccess.DataBaseConfigulation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CoffeeShop.DataAccess.DataAccess.DataBaseContext
{
    public class CoffeeShopContextFactory : IDesignTimeDbContextFactory<CoffeeShopContext>
    {
        public CoffeeShopContext CreateDbContext(string[] args)
        {
            AppConfiguration appConfiguration = new AppConfiguration();
            var optionsBuilder = new DbContextOptionsBuilder<CoffeeShopContext>();
            optionsBuilder.UseSqlServer(appConfiguration.SqlConnectionString);
            return new CoffeeShopContext(optionsBuilder.Options);
        }
    }
}
