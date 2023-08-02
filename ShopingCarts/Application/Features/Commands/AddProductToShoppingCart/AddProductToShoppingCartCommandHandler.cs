using AutoMapper;

using MediatR;

using ShopingCarts.Application.Contracts;
using ShopingCarts.Application.Features.Common;

namespace ShopingCarts.Application.Features.Commands.AddProductToShoppingCart
{
    internal class AddProductToShoppingCartCommandHandler : CommandBase, IRequestHandler<AddProductToShoppingCartCommand, AddProductToShoppingCartCommandResult>
    {
        public AddProductToShoppingCartCommandHandler(IShoppingCartContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public Task<AddProductToShoppingCartCommandResult> Handle(AddProductToShoppingCartCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
