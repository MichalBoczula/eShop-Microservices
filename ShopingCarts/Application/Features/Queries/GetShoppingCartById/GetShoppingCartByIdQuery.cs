using MediatR;

namespace ShopingCarts.Application.Features.Queries.GetShoppingCartById
{
    internal class GetShoppingCartByIdQuery : IRequest<GetShoppingCartByIdQueryResult>
    {
        public GetShoppingCartByIdQueryExternal ExternalContract { get; set; }
    }
}
