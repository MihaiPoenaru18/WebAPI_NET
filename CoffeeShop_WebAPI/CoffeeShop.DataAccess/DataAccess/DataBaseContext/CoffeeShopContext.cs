using CoffeeShop.DataAccess.DataAccess.ModelDB.Order;
using CoffeeShop.DataAccess.DataAccess.ModelDB.ProductModel;
using CoffeeShop.DataAccess.DataAccess.ModelDB.User;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.DataAccess.DataAccess.DataBaseContext
{
    public class CoffeeShopContext : DbContext
    {
        public CoffeeShopContext(DbContextOptions<CoffeeShopContext> options) : base(options) 
        {
            this.ChangeTracker.LazyLoadingEnabled = true;
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserWithNewsLetter> Newsletters { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Promotion> Promotion { get; set; }
        public DbSet<Order> Order { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Products)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(o => o.OrderId)
                      .HasColumnName("OrderId")
                      .HasDefaultValueSql("NEWID()");

                entity.HasKey(o => o.OrderId);

                entity.HasMany(o => o.Products)
                      .WithOne();
                      //.HasForeignKey(p => p.OrderId);

                entity.HasOne(o => o.Address)
                      .WithMany();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Id)
                      .HasDefaultValueSql("NEWID()");

                entity.HasKey(u => u.Id);

                entity.Property(u => u.Email).IsRequired();
                entity.Property(u => u.FirstName).IsRequired();
                entity.Property(u => u.LastName).IsRequired();
                entity.Property(u => u.Role).IsRequired();
                entity.Property(u => u.Password).IsRequired();

                entity.HasOne(u => u.UserWithNewsLetter)
                      .WithOne(n => n.User)
                      .HasForeignKey<User>(u => u.IdUserNewsLetter);
            });

            modelBuilder.Entity<UserWithNewsLetter>(entity =>
            {
                entity.Property(n => n.Id)
                      .HasDefaultValueSql("NEWID()");

                entity.HasKey(n => n.Id);

                entity.Property(n => n.Name).IsRequired();
                entity.Property(n => n.Email).IsRequired();
                entity.Property(n => n.IsNewsLetterActive).IsRequired();
            });
        }
    }
}
