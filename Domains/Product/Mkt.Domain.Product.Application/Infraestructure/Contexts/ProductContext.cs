using Microsoft.EntityFrameworkCore;
using Mkt.Domain.Product.Application.Infraestructure.Entities;
using Mkt.Domain.Product.Application.Infraestructure.Mappings;
using System.Diagnostics;

namespace Mkt.Domain.Product.Application.Infraestructure.Contexts
{
    public class ProductContext : DbContext
    {
        public DbSet<ProductEntity> Products { get; set; }

        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductMapping());
        }
    }
}
