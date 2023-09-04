using Microsoft.AspNetCore.Mvc;
using ShopingCarts.Application.Features.Commands.AddProductToShoppingCart;
using ShopingCarts.Application.Features.Commands.RemoveProductFromShoppingCart;
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

        [HttpPost("AddProductToShoppingCart")]
        public async Task<ActionResult> GetShoppingCartById([FromBody] AddProductToShoppingCartCommandExternal contract)
        {
            var result = await Mediator.Send(new AddProductToShoppingCartCommand()
            {
                ExternalContract = contract
            });
            return Ok(result);
        }

        [HttpDelete("RemoveProductFromShoppingCart")]
        public async Task<ActionResult> GetShoppingCartById([FromBody] RemoveProductFromShoppingCartCommandExternal contract)
        {
            var result = await Mediator.Send(new RemoveProductFromShoppingCartCommand()
            {
                ExternalContract = contract
            });
            return Ok(result);
        }
    }
}