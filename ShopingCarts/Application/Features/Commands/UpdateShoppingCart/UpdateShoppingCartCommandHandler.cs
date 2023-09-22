using AutoMapper;
using Integrations.ShoppingCart;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShopingCarts.Application.Contracts;
using ShopingCarts.Application.Features.Commands.AddProductToShoppingCart;
using ShopingCarts.Application.Features.Common;
using ShopingCarts.Domain.Entities;
using ShopingCarts.ExternalServices.SynchComunication.HttpClients.Products.Abstract;

namespace ShopingCarts.Application.Features.Commands.UpdateShoppingCart
{
    internal class UpdateShoppingCartCommandHandler : CommandBase, IRequestHandler<UpdateShoppingCartCommand, UpdateShoppingCartCommandResult>
    {
        private readonly IProductsHttpRequestHandler _productsHttpRequestHandler;

        public UpdateShoppingCartCommandHandler(IShoppingCartContext context, IMapper mapper, IProductsHttpRequestHandler productsHttpRequestHandler) : base(context, mapper)
        {
            this._productsHttpRequestHandler = productsHttpRequestHandler;
        }

        public async Task<UpdateShoppingCartCommandResult> Handle(UpdateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var shoppingCart = await this._context.ShoppingCarts
                    .Where(x => x.IntegrationId == request.ShoppingCartIntegrationId)
                    .FirstOrDefaultAsync();

                if (shoppingCart == null)
                {
                    return new UpdateShoppingCartCommandResult
                    {
                        PositiveMessage = null,
                        ErrorDescription = $"Shopping cart identify by integrationId {request.ShoppingCartIntegrationId} doesn't exist"
                    };
                }

                var products = this._mapper.Map<List<ShoppingCartProduct>>(request.Products);
                var productsToRemove = products.Where(x => x.Quantity == 0).ToList();

                if (productsToRemove.Any())
                {
                    this._context.ShoppingCartProducts.RemoveRange(productsToRemove);
                }

                var productsToAdd = products.Where(x => x.Quantity > 0).ToList();

                if (productsToAdd.Any())
                {
                    await Parallel.ForEachAsync(
                        productsToAdd,
                        cancellationToken,
                        async (product, token) =>
                            {
                                var IsProductExists =
                                    await this._productsHttpRequestHandler.GetProductsByIntegrationIds(
                                        new List<Guid>() { product.ProductIntegrationId });

                                if (IsProductExists.Products != null && IsProductExists.Products.Any())
                                {
                                    await this._context.ShoppingCartProducts.AddAsync(product, token);
                                }
                            });
                }

                await this._context.SaveChangesAsync(cancellationToken);

                return new UpdateShoppingCartCommandResult
                {
                    PositiveMessage = $"Successfully updated shopping cart identify by integrationId {shoppingCart.IntegrationId}",
                    ErrorDescription = null
                };
            }
            catch (Exception ex)
            {
                return new UpdateShoppingCartCommandResult
                {
                    PositiveMessage = null,
                    ErrorDescription = ex.Message
                };
            }
        }
    }
}
