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
                    .Include(x => x.ShoppingCartProducts)
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

                var ids = request.Products.Where(x => x.Quantity == 0).Select(x => x.Id).ToList();

                var productsToRemove = shoppingCart.ShoppingCartProducts.Where(x => ids.Contains(x.Id)).ToList();

                if (productsToRemove.Any())
                {
                    this._context.ShoppingCartProducts.RemoveRange(productsToRemove);
                }

                //var productsToProccess = products.Where(x => x.Quantity > 0).ToList();

                //if (productsToProccess.Any())
                //{
                //    await Parallel.ForEachAsync(
                //        productsToProccess,
                //        cancellationToken,
                //        async (product, token) =>
                //            {
                //                var IsProductExists =
                //                    await this._productsHttpRequestHandler.GetProductsByIntegrationIds(
                //                        new List<Guid>() { productsToProccess.Select(x =>  x.});

                //                if (IsProductExists.Products != null && IsProductExists.Products.Any())
                //                {
                //                    await this._context.ShoppingCartProducts.AddAsync(product, token);
                //                }
                //            });
                //
                //}

                await this._context.SaveChangesAsync(cancellationToken);


                shoppingCart = await this._context.ShoppingCarts
                    .Include(x => x.ShoppingCartProducts)
                    .Where(x => x.IntegrationId == request.ShoppingCartIntegrationId)
                    .FirstOrDefaultAsync();

                shoppingCart.Total = shoppingCart.ShoppingCartProducts.Aggregate(0, (total, prod) => total = prod.Price * prod.Quantity); 
                
                this._context.ShoppingCarts.Update(shoppingCart);
                
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
