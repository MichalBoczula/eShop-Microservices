using Microsoft.EntityFrameworkCore;
using ShopingCarts.Domain.Entities;

namespace ShopingCarts.Application.Contracts
{
    internal interface IShoppingCartContext
    {
        DbSet<ShoppingCart> ShoppingCarts { get; set; }
        DbSet<ShoppingCartProduct> ShoppingCartProducts { get; set; }
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
