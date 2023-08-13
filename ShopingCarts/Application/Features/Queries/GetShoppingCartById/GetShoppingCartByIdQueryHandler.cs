using AutoMapper;
using AutoMapper.QueryableExtensions;

using MediatR;

using Microsoft.EntityFrameworkCore;

using ShopingCarts.Application.Contracts;
using ShopingCarts.Application.Features.Common;
using ShopingCarts.Application.Features.Queries.GetShoppingCartById.Dtos;

namespace ShopingCarts.Application.Features.Queries.GetShoppingCartById
{
    internal class GetShoppingCartByIdQueryHandler : QueryBase, IRequestHandler<GetShoppingCartByIdQuery, GetShoppingCartByIdQueryResult>
    {
        public GetShoppingCartByIdQueryHandler(IShoppingCartContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public async Task<GetShoppingCartByIdQueryResult> Handle(GetShoppingCartByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await this._context.ShoppingCarts
                                 .Include(x => x.ShoppingCartProducts)
                                 .Where(x => x.Id == request.ExternalContract.ShoppingCartId)
                                 .ProjectTo<ShoppingCartDto>(this._mapper.ConfigurationProvider)
                                 .FirstOrDefaultAsync(cancellationToken);

                if (result is null) 
                {
                    return new GetShoppingCartByIdQueryResult { ShoppingCartDto = null, ErrorDescription = $"Shopping cart identify by Id: {request.ExternalContract.ShoppingCartId} doesn't exist."};
                }
                else
                {
                    return new GetShoppingCartByIdQueryResult { ShoppingCartDto = result, ErrorDescription = null };
                }
            }
            catch (Exception ex)
            {
                return new GetShoppingCartByIdQueryResult { ShoppingCartDto = null, ErrorDescription = ex.Message };
            }
        }
    }
}
