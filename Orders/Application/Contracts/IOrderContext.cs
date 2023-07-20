using Microsoft.EntityFrameworkCore;
using Orders.Domain.Entities;

namespace Orders.Application.Contracts
{
    internal interface IOrderContext
    {
        DbSet<Order> Orders { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<OrderProduct> OrderProducts { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
