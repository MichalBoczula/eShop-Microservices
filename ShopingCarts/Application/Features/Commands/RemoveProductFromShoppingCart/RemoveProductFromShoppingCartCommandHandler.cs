using AutoMapper;

using MediatR;

using ShopingCarts.Application.Contracts;
using ShopingCarts.Application.Features.Common;

namespace ShopingCarts.Application.Features.Commands.RemoveProductFromShoppingCart
{
    internal class RemoveProductFromShoppingCartCommandHandler : CommandBase, IRequestHandler<RemoveProductFromShoppingCartCommand, RemoveProductFromShoppingCartCommandResult>
    {
        public RemoveProductFromShoppingCartCommandHandler(IShoppingCartContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public Task<RemoveProductFromShoppingCartCommandResult> Handle(RemoveProductFromShoppingCartCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
