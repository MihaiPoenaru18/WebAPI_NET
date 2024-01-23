using CoffeeShop.DataAccess.DataAccess.DataBaseContext;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.DataAccess.DataAccess.DataBaseConfigulation
{
    public class OptionsBuild
    {
        public OptionsBuild()
        {
            Settings = new AppConfiguration();
            OpsBuilder = new DbContextOptionsBuilder<CoffeeShopContext>();
            OpsBuilder.UseSqlServer(Settings.SqlConnectionString);
            dbOptions = OpsBuilder.Options;
        }
        public DbContextOptionsBuilder<CoffeeShopContext> OpsBuilder { get; set; }
        public DbContextOptions<CoffeeShopContext> dbOptions { get; set; }
        public AppConfiguration Settings { get; set; }
    }
}
