using Microsoft.AspNetCore.Mvc;
using ShopingCarts.Application.Features.Queries.GetShoppingCartById;
using ShopingCarts.Controllers.Base;

namespace ShopingCarts.Controllers
{
    [Route("[controller]")]
    public class ShoppingCartsController : BaseController
    {
        [HttpPost("GetShoppingCartById")]
        public async Task<ActionResult> GetShoppingCartById(GetShoppingCartByIdQueryExternal contract)
        {
            var result = await Mediator.Send(new GetShoppingCartByIdQuery()
            {
                ExternalContract = contract
            });
            return Ok(result);
        }
    }
}