using CoffeeShop.DataAccess.DataAccess.ModelDB.Order;
using CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel;
using CoffeeShop.DataAccess.DataAccess.ModelDB.User;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.DataAccess.DataAccess.DataBaseContext
{
    public class CoffeeShopContext : DbContext
    {
        public CoffeeShopContext(DbContextOptions<CoffeeShopContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<UserWithNewsLetter> Newsletters { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        //public DbSet<Order> Order { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
           .HasOne(p => p.Category)
           .WithMany(c => c.Products)
           .HasForeignKey(p => p.IdCategory)
           .OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<List<Guid>>().HasNoKey();
            modelBuilder.Entity<User>(entity =>
            {
                // Primary key
                entity.Property(n => n.Id)
                    .HasDefaultValueSql("NEWID()"); // Use SQL Server to generate a new Guid
                entity.HasKey(n => n.Id);

                // Required fields
                entity.Property(u => u.Email).IsRequired();
                entity.Property(u => u.FirstName).IsRequired();
                entity.Property(u => u.LastName).IsRequired();
                entity.Property(u => u.Role).IsRequired();
                entity.Property(u => u.Password).IsRequired();

                // Relationship with UserWithNewsLetter
                entity.HasOne(u => u.UserWithNewsLetter)
                    .WithOne(n => n.User)
                    .HasForeignKey<User>(u => u.IdUserNewsLetter);
            });

            modelBuilder.Entity<UserWithNewsLetter>(entity =>
            {
                // Primary key with automatic Guid generation
                entity.Property(n => n.Id)
                    .HasDefaultValueSql("NEWID()"); // Use SQL Server to generate a new Guid
                entity.HasKey(n => n.Id);

                // Additional configuration for UserWithNewsLetter entity
                entity.Property(n => n.Name).IsRequired();
                entity.Property(n => n.Email).IsRequired();
                entity.Property(n => n.IsNewsLetterActive).IsRequired();
            });
        }

       

       
    }
}
