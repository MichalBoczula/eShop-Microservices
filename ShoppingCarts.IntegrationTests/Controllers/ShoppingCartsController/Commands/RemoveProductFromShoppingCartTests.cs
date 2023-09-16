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
    public class RemoveProductFromShoppingCartTests : IClassFixture<ShoppingCartsWebAppFactoryForRemoveProducts<Program>>
    {
        private readonly HttpClient _client;

        public RemoveProductFromShoppingCartTests(ShoppingCartsWebAppFactoryForRemoveProducts<Program> factory)
        {
            this._client = factory.CreateClient();
        }

        [Fact]
        public async Task ShouldReduceQuantityOfProductsFromShoppingCart()
        {
            //arrange & act
            var response = await _client.DeleteAsync("ShoppingCarts/2/Products/11");

            //assert 
            response.EnsureSuccessStatusCode();
            var result = await Utilities.GetResponseContent<RemoveProductFromShoppingCartCommandResult>(response);
            result.PositiveMessage.Should().NotBeNull();
            result.PositiveMessage.Should().Be("Successfullyy removed with id 11 product from shoppingCart identify by 2");
            result.ErrorDescription.Should().BeNull();

            var shoppingCart = await Helper.GetShoppingCartById(this._client);

            shoppingCart.ShoppingCartDto.Should().NotBeNull();
            shoppingCart.ShoppingCartDto.Total.Should().Be(3000);
            shoppingCart.ShoppingCartDto.ShoppingCartProducts.Should().HaveCount(1);
            shoppingCart.ErrorDescription.Should().BeNull();
        }
    }
}
