using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Products.Application.Contracts.Persistance;
using Products.Application.Features.Common;

namespace Products.Application.Features.Queries.GetAllProducts
{
    internal class GetAllProductsQueryHandler : QueryBase, IRequestHandler<GetAllProductsQuery, GetAllProductsQueryResult>
    {
        public GetAllProductsQueryHandler(IProductsContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<GetAllProductsQueryResult> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await this._context.Products.ToListAsync();
                var result = this._mapper.Map<List<ProductDto>>(products);
                return new GetAllProductsQueryResult { Products = result, ErrorDescription = null };
            }
            catch (Exception ex)
            {
                return new GetAllProductsQueryResult { Products = null, ErrorDescription = ex.Message };
            }
        }
    }
}
