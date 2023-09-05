using AutoMapper;

using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopingCarts.Application.Contracts;
using ShopingCarts.Application.Features.Common;
using ShopingCarts.Domain.Entities;
using ShopingCarts.ExternalServices.SynchComunication.HttpClients.Concrete.Products;
using ShopingCarts.ExternalServices.SynchComunication.HttpClients.Products.Abstract;

namespace ShopingCarts.Application.Features.Commands.AddProductToShoppingCart
{
    internal class AddProductToShoppingCartCommandHandler : CommandBase, IRequestHandler<AddProductToShoppingCartCommand, AddProductToShoppingCartCommandResult>
    {
        private readonly IProductsHttpRequestHandler _productsHttpRequestHandler;

        public AddProductToShoppingCartCommandHandler(IShoppingCartContext context, IMapper mapper, IProductsHttpRequestHandler productsHttpRequestHandler)
            : base(context, mapper)
        {
            _productsHttpRequestHandler = productsHttpRequestHandler;
        }

        public async Task<AddProductToShoppingCartCommandResult> Handle(AddProductToShoppingCartCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var shoppingCart = await this._context.ShoppingCarts
               .Include(x => x.ShoppingCartProducts)
               .FirstOrDefaultAsync(x => x.Id == request.ShoppingCartId, cancellationToken);

                if (shoppingCart.ShoppingCartProducts.Any(x => x.Id == request.ExternalContract.ShoppingCartProductId))
                {
                    var product = shoppingCart.ShoppingCartProducts
                        .First(x => x.Id == request.ExternalContract.ShoppingCartProductId);
                    product.Quantity += request.ExternalContract.ShoppingCartProductQuantity;

                    this._context.ShoppingCartProducts.Update(product);
                    await this._context.SaveChangesAsync(cancellationToken);

                    return new AddProductToShoppingCartCommandResult()
                    {
                        PositiveMessage = $"Updated product with Id {product.Id}",
                        ErrorDescription = null
                    };
                }
                else
                {
                    if (request.ExternalContract.ShoppingCartProductIntegrationId.HasValue)
                    {
                        var contract = new List<Guid>
                        {
                            request.ExternalContract.ShoppingCartProductIntegrationId.Value
                        };

                        var result = await this._productsHttpRequestHandler.GetProductsByIntegrationIds(contract);

                        if (result.Products.Any())
                        {
                            var shoppingCartProduct = this._mapper.Map<ShoppingCartProduct>((request.ShoppingCartId, request.ExternalContract, result.Products.First()));
                          
                            this._context.ShoppingCartProducts.Add(shoppingCartProduct);
                            await this._context.SaveChangesAsync(cancellationToken);

                            return new AddProductToShoppingCartCommandResult()
                            {
                                PositiveMessage = $"Added new product with Id {shoppingCartProduct.Id}",
                                ErrorDescription = null
                            };
                        }
                    }
                    return new AddProductToShoppingCartCommandResult()
                    {
                        PositiveMessage = null,
                        ErrorDescription = "Product doesn't exist"
                    };
                }
            }
            catch (Exception ex)
            {
                return new AddProductToShoppingCartCommandResult()
                {
                    PositiveMessage = null,
                    ErrorDescription = ex.Message
                };
            }
        }
    }
}
