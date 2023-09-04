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
        [HttpPost("GetShoppingCartById")]
        public async Task<ActionResult> GetShoppingCartById([FromBody] GetShoppingCartByIdQueryExternal contract)
        {
            var result = await Mediator.Send(new GetShoppingCartByIdQuery()
            {
                ExternalContract = contract
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