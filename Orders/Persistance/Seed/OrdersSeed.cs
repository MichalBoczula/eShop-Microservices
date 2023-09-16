using Microsoft.EntityFrameworkCore;
using Orders.Domain.Entities;

namespace Orders.Persistance.Seed
{
    internal static class OrdersSeed
    {
        internal static void SeedOrders(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
              new User
              {
                  Id = 1,
                  IntegrationId = new Guid("95464765-CF3F-4ED7-B353-5D2F810DCC33")
              }
          );
            modelBuilder.Entity<Order>().HasData(
              new Order
              {
                  Id = 1,
                  IntegrationId = Guid.NewGuid(),
                  UserId = 1,
                  Total = 2000
              }
          );
            modelBuilder.Entity<OrderProduct>().HasData(
               new OrderProduct
               {
                   Id = 1,
                   ProductIntegrationId = new Guid("55CCEE28-E15D-4644-A7BE-2F8A93568D6F"),
                   Quantity = 1,
                   Price = 2000,
                   OrderId = 1,
               }
           );
        }
    }
}
