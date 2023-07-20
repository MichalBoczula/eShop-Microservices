using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using ShopingCarts.Application.Contracts;

using Microsoft.EntityFrameworkCore;
using ShopingCarts.Persistance.Seed;
using ShopingCarts.Domain.Entities;

namespace ShopingCarts.Persistance.Context
{
    internal class ShoppingCartContext : DbContext, IShoppingCartContext
    {
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartProduct> ShoppingCartProducts { get; set; }
        public DbSet<User> Users { get; set; }

        public ShoppingCartContext([NotNull] DbContextOptions<ShoppingCartContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.SeedShoppingCarts();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}