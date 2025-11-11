using Infrastructure.Products.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Products.Context
{
    public class ProductsContext : DbContext
    {
        public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductsContext).Assembly);
        }
    }
}
