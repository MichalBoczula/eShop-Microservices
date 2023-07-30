using Microsoft.EntityFrameworkCore;
using ShopingCarts.Domain.Entities;

namespace ShopingCarts.Persistance.Seed
{
    internal static class ShoppingCartsSeed
    {
        internal static void SeedShoppingCarts(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
              new User
              {
                  Id = 1,
                  IntegrationId = Guid.NewGuid(),
                  ShoppingCartId = 1,
              }
          );
            modelBuilder.Entity<ShoppingCart>().HasData(
              new ShoppingCart
              {
                  Id = 1,
                  IntegrationId = Guid.NewGuid(),
                  UserId = 1,
                  Total = 3000
              }
          );
            modelBuilder.Entity<ShoppingCartProduct>().HasData(
               new ShoppingCartProduct
               {
                   Id = 1,
                   ProductIntegrationId = new Guid("0EF1268E-33D6-49CD-A4B5-8EB94494D896"),
                   Quantity = 1,
                   Total = 3000,
                   ShoppingCartId = 1,
               }
           );
        }
    }
}
