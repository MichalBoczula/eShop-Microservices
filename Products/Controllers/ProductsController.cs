using Microsoft.AspNetCore.Mvc;
using Products.Application.Features.Queries.GetAllProducts;
using Products.Application.Features.Queries.GetProductsByIntegrationId;
using Products.Controllers.Base;

namespace Products.Controllers
{
    [Route("[controller]")]
    public class ProductsController : BaseController
    {
        [HttpGet("GetProducts")]
        public async Task<ActionResult> GetProducts()
        {
            var result = await Mediator.Send(new GetAllProductsQuery());
            return Ok(result);
        }

        [HttpPost("GetProductsByIntegrationId")]
        public async Task<ActionResult> GetProductsByIntegrationId([FromBody] GetProductsByIntegrationIdQueryExternal contract)
        {
            var result = await Mediator.Send(new GetProductsByIntegrationIdQuery(){ ExternalContract = contract });
            return Ok(result);
        }
    }
}
