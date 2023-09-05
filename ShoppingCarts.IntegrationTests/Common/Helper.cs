using ShopingCarts.Application.Features.Queries.GetShoppingCartById;
using System.Net.Http.Json;

namespace ShoppingCarts.IntegrationTests.Common
{
    internal static class Helper
    {
        public static async Task<GetShoppingCartByIdQueryResult> GetShoppingCartById(HttpClient client)
        {
            //arrange & act
            var response = await client.GetAsync("ShoppingCarts/1");

            //assert
            response.EnsureSuccessStatusCode();
            var result = await Integrations.Common.Utilities.GetResponseContent<GetShoppingCartByIdQueryResult>(response);
            return result;
        }
    }
}
