using FluentAssertions;
using Integrations.Common;
using ShopingCarts.Application.Features.Queries.GetShoppingCartById;
using ShoppingCarts.IntegrationTests.Configuration;
using System.Net.Http.Json;

namespace ShoppingCarts.IntegrationTests.Controllers.ShoppingCartsController.Queries
{
    public class ShoppingCartsControllerTests : IClassFixture<ShoppingCartsWebAppFactory<Program>>
    {
        private readonly HttpClient _client;

        public ShoppingCartsControllerTests(ShoppingCartsWebAppFactory<Program> factory)
        {
            this._client = factory.CreateClient();
        }

        [Fact]
        public async Task ShouldReturnShoppingCart()
        {
            //arrange
            var contract = new GetShoppingCartByIdQueryExternal { ShoppingCartId = 1 };

            //act
            var response = await _client.PostAsJsonAsync("ShoppingCarts/GetShoppingCartById", contract);

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
            //arrange
            var contract = new GetShoppingCartByIdQueryExternal { ShoppingCartId = -1 };

            //act
            var response = await _client.PostAsJsonAsync("ShoppingCarts/GetShoppingCartById", contract);

            //assert
            response.EnsureSuccessStatusCode();
            var result = await Utilities.GetResponseContent<GetShoppingCartByIdQueryResult>(response);
            result.ShoppingCartDto.Should().BeNull();
            result.ErrorDescription.Should().Be("Shopping cart identify by Id: -1 doesn't exist.");
        }
    }
}
