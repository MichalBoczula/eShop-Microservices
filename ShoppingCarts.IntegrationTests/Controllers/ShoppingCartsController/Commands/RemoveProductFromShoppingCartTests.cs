using FluentAssertions;
using Integrations.Common;
using Newtonsoft.Json;
using ShopingCarts.Application.Features.Commands.AddProductToShoppingCart;
using ShopingCarts.Application.Features.Commands.RemoveProductFromShoppingCart;
using ShoppingCarts.IntegrationTests.Common;
using ShoppingCarts.IntegrationTests.Configuration;
using System.Net.Http.Json;
using System.Text;

namespace ShoppingCarts.IntegrationTests.Controllers.ShoppingCartsController.Commands
{
    public class RemoveProductFromShoppingCartTests : IClassFixture<ShoppingCartsWebAppFactory<Program>>
    {
        private readonly HttpClient _client;

        public RemoveProductFromShoppingCartTests(ShoppingCartsWebAppFactory<Program> factory)
        {
            this._client = factory.CreateClient();
        }

        [Fact]
        public async Task ShouldReduceQuantityOfProductsFromShoppingCart()
        {
            //arrange
            var contractAdd = new RemoveProductFromShoppingCartCommandExternal
            {
                ShoppingCartId = 1,
                ShoppingCartProductId = 1,
                ShoppingCartProductQuantity = 1
            };

            //act
            var request = new HttpRequestMessage(HttpMethod.Delete, "ShoppingCarts/RemoveProductFromShoppingCart");
            request.Content = new StringContent(JsonConvert.SerializeObject(contractAdd));
            var response = await this._client.SendAsync(request);
            //var responseAdd = await _client.DeleteAsync("ShoppingCarts/RemoveProductFromShoppingCart", contractAdd);

            //assert 
            response.EnsureSuccessStatusCode();
            var result = await Utilities.GetResponseContent<RemoveProductFromShoppingCartCommandResult>(response);
            result.PositiveMessage.Should().NotBeNull();
            result.PositiveMessage.Should().Be("Updated product with Id 1");
            result.ErrorDescription.Should().BeNull();

            var shoppingCart = await Helper.GetShoppingCartById(this._client);

            shoppingCart.ShoppingCartDto.Should().NotBeNull();
            shoppingCart.ShoppingCartDto.ShoppingCartProducts
                .First(x => x.ProductIntegrationId == new Guid("0ef1268e-33d6-49cd-a4b5-8eb94494d896")).Quantity
                .Should().Be(2);
            shoppingCart.ErrorDescription.Should().BeNull();
        }
    }
}
