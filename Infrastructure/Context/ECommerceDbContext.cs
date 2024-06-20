using APIBrechoRFCC.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace APIBrechoRFCC.Infrastructure.Context
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }   
        public DbSet<ProductOption> ProductOptions { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartLine> CartLines { get; set; }
        public DbSet<HomeBanner> HomeBanners { get; set; }
        public DbSet<HomeSection> HomeSections { get; set; }
        public DbSet<Customer> Customers { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
