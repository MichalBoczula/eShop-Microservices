using ShopingCarts.Application.Features.Queries.GetShoppingCartById;
using System.Net.Http.Json;

namespace ShoppingCarts.IntegrationTests.Common
{
    internal static class Helper
    {
        public static async Task<GetShoppingCartByIdQueryResult> GetShoppingCartById(HttpClient client)
        {
            //arrange
            var contract = new GetShoppingCartByIdQueryExternal { ShoppingCartId = 1 };

            //act
            var response = await client.PostAsJsonAsync("ShoppingCarts/GetShoppingCartById", contract);

            //assert
            response.EnsureSuccessStatusCode();
            var result = await Integrations.Common.Utilities.GetResponseContent<GetShoppingCartByIdQueryResult>(response);
            return result;
        }
    }
}
