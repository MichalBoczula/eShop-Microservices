﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Products.Application.Contracts.Persistance;
using Products.Application.Features.Common;
using Products.Application.Features.Queries.GetAllProducts;

namespace Products.Application.Features.Queries.GetProductsByIntegrationId
{
    internal class GetProductsByIntegrationIdQueryHandler : QueryBase, IRequestHandler<GetProductsByIntegrationIdQuery, GetProductsByIntegrationIdQueryResult>
    {
        public GetProductsByIntegrationIdQueryHandler(IProductsContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<GetProductsByIntegrationIdQueryResult> Handle(GetProductsByIntegrationIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _context.Products
                    .Where(x => request.ExternalContract.IntegrationIds.Contains(x.IntegrationId))
                    .ToListAsync();
                var result = this._mapper.Map<List<ProductDto>>(products);
                return new GetProductsByIntegrationIdQueryResult { Products = result, ErrorDescription = null };
            }
            catch (Exception ex)
            {
                return new GetProductsByIntegrationIdQueryResult { Products = null, ErrorDescription = ex.Message };
            }
        }
    }
}