using Integrations.ShoppingCart;
using Microsoft.AspNetCore.Mvc;
using ShopingCarts.Application.Features.Commands.AddProductToShoppingCart;
using ShopingCarts.Application.Features.Commands.RemoveProductFromShoppingCart;
using ShopingCarts.Application.Features.Commands.UpdateShoppingCart;
using ShopingCarts.Application.Features.Queries.GetShoppingCartById;
using ShopingCarts.Controllers.Base;

namespace ShopingCarts.Controllers
{
    [Route("[controller]")]
    public class ShoppingCartsController : BaseController
    {
        [HttpGet("{shoppingCartId}")]
        public async Task<ActionResult> GetShoppingCartById(int shoppingCartId)
        {
            var result = await Mediator.Send(new GetShoppingCartByIdQuery()
            {
                ExternalContract = new GetShoppingCartByIdQueryExternal
                {
                    ShoppingCartId = shoppingCartId
                }
            });
            return Ok(result);
        }

        [HttpPost("{shoppingCartId}")]
        public async Task<ActionResult> AddProductToShoppingCart(int shoppingCartId, [FromBody] AddProductToShoppingCartCommandExternal contract)
        {
            var result = await Mediator.Send(new AddProductToShoppingCartCommand()
            {
                ShoppingCartId = shoppingCartId,
                ExternalContract = contract
            });
            return Ok(result);
        }

        [HttpDelete("{shoppingCartId}/Products/{productId}")]
        public async Task<ActionResult> RemoveProductFromShoppingCart(int shoppingCartId, int productId)
        {
            var result = await Mediator.Send(new RemoveProductFromShoppingCartCommand()
            {
                ShoppingCartId  = shoppingCartId,
                ShoppingCartProductId = productId

            });
            return Ok(result);
        }

        [HttpPut("{shoppingCartInteId}")]
        public async Task<ActionResult> RemoveProductFromShoppingCart(Guid shoppingCartId, [FromBody] List<ShoppingCartProductExternal> contract)
        {
            var result = await Mediator.Send(new UpdateShoppingCartCommand()
            {
                ShoppingCartIntegrationId = shoppingCartId,
                Products = contract
            });
            return Ok(result);
        }
    }
}