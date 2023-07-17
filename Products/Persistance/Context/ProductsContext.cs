using Microsoft.EntityFrameworkCore;
using Products.Application.Contracts.Persistance;
using Products.Domain.Entities;
using Products.Persistance.Seed;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Products.Persistance.Context
{
    internal class ProductsContext : DbContext, IProductsContext
    {
        public ProductsContext([NotNull] DbContextOptions<ProductsContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.SeedProducts();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
