using FluentAssertions;
using Integrations.Common;
using ShopingCarts.Application.Features.Queries.GetShoppingCartById;
using ShoppingCarts.IntegrationTests.Configuration;

namespace ShoppingCarts.IntegrationTests.Controllers.ShoppingCartsController.Queries
{
    public class ShoppingCartsControllerTests : IClassFixture<ShoppingCartsWebAppFactoryForAddProducts<Program>>
    {
        private readonly HttpClient _client;

        public ShoppingCartsControllerTests(ShoppingCartsWebAppFactoryForAddProducts<Program> factory)
        {
            this._client = factory.CreateClient();
        }

        [Fact]
        public async Task ShouldReturnShoppingCart()
        {
            //arrange & act
            var response = await _client.GetAsync("ShoppingCarts/1");

            //assert
            response.EnsureSuccessStatusCode();
            var result = await Utilities.GetResponseContent<GetShoppingCartByIdQueryResult>(response);
            result.ShoppingCartDto.Should().NotBeNull();
            result.ShoppingCartDto.ShoppingCartProducts.Should().HaveCount(1);
            result.ErrorDescription.Should().BeNull();
        }

        [Fact]
        public async Task ShouldReturnErrorMessage()
        {
            //arrange & act
            var response = await _client.GetAsync("ShoppingCarts/-1");

            //assert
            response.EnsureSuccessStatusCode();
            var result = await Utilities.GetResponseContent<GetShoppingCartByIdQueryResult>(response);
            result.ShoppingCartDto.Should().BeNull();
            result.ErrorDescription.Should().Be("Shopping cart identify by Id: -1 doesn't exist.");
        }
    }
}
