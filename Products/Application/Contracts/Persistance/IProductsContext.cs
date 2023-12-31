﻿using Microsoft.EntityFrameworkCore;
using Products.Domain.Entities;

namespace Products.Application.Contracts.Persistance
{
    internal interface IProductsContext
    {
        DbSet<Product> Products { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
