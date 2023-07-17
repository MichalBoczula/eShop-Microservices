using Microsoft.EntityFrameworkCore;
using Products.Domain.Entities;

namespace Products.Persistance.Seed
{
    public static class ProductsSeed
    {
        public static void SeedProducts(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Chinese",
                    ImgName = "Huawei",
                    Price = 2000,
                    IntegrationId = Guid.NewGuid()
                },
                new Product
                {
                    Id = 2,
                    Name = "Samsung",
                    ImgName = "Samsung",
                    Price = 3000,
                    IntegrationId = Guid.NewGuid()
                },
                new Product
                {
                    Id = 3,
                    Name = "Iphone",
                    ImgName = "Iphone",
                    Price = 4000,
                    IntegrationId = Guid.NewGuid()
                }
            );
        }
    }
}
