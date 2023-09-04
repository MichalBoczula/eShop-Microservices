﻿using FluentAssertions;
using Integrations.Common;
using ShopingCarts.Application.Features.Commands.AddProductToShoppingCart;
using ShopingCarts.Application.Features.Queries.GetShoppingCartById;
using ShoppingCarts.IntegrationTests.Configuration;
using System.Net.Http.Json;

namespace ShoppingCarts.IntegrationTests.Controllers.ShoppingCartsController.Commands
{
    public class AddProductToShoppingCartTests : IClassFixture<ShoppingCartsWebAppFactory<Program>>
    {
        private readonly HttpClient _client;

        public AddProductToShoppingCartTests(ShoppingCartsWebAppFactory<Program> factory)
        {
            this._client = factory.CreateClient();
        }

        [Fact]
        public async Task ShouldAddAnotherAdditionalProductToShoppingCart()
        {
            //arrange
            var contractAdd = new AddProductToShoppingCartCommandExternal
            {
                ShoppingCartId = 1,
                ShoppingCartProductId = 1,
                ShoppingCartProductQuantity = 1
            };

            //act
            var responseAdd = await _client.PostAsJsonAsync("ShoppingCarts/AddProductToShoppingCart", contractAdd);

            //assert 
            responseAdd.EnsureSuccessStatusCode();
            var result = await Utilities.GetResponseContent<AddProductToShoppingCartCommandResult>(responseAdd);
            result.PositiveMessage.Should().NotBeNull();
            result.PositiveMessage.Should().Be("Updated product with Id 1");
            result.ErrorDescription.Should().BeNull();

            var shoppingCart = await this.GetShoppingCartById();

            shoppingCart.ShoppingCartDto.Should().NotBeNull();
            shoppingCart.ShoppingCartDto.ShoppingCartProducts
                .First(x => x.ProductIntegrationId == new Guid("0ef1268e-33d6-49cd-a4b5-8eb94494d896")).Quantity
                .Should().Be(2);
            shoppingCart.ErrorDescription.Should().BeNull();
        }

        [Fact]
        public async Task ShouldAddAnotherNewProductToShoppingCart()
        {
            //arrange
            var contract = new AddProductToShoppingCartCommandExternal
            {
                ShoppingCartId = 1,
                ShoppingCartProductIntegrationId = new Guid("23363AFF-DD71-4F3C-8381-F7E71021761E"),
                ShoppingCartProductQuantity = 1
            };
            var contractGet = new GetShoppingCartByIdQueryExternal { ShoppingCartId = 1 };

            //act
            var responseAdd = await _client.PostAsJsonAsync("ShoppingCarts/AddProductToShoppingCart", contract);

            //assert
            responseAdd.EnsureSuccessStatusCode();
            var result = await Utilities.GetResponseContent<AddProductToShoppingCartCommandResult>(responseAdd);
            result.PositiveMessage.Should().NotBeNull();
            result.PositiveMessage.Should().Be("Added new product with Id 2");
            result.ErrorDescription.Should().BeNull();

            var shoppingCart = await this.GetShoppingCartById();

            shoppingCart.ShoppingCartDto.Should().NotBeNull();
            shoppingCart.ShoppingCartDto.ShoppingCartProducts.Should().HaveCount(2);
            shoppingCart.ErrorDescription.Should().BeNull();
        }

        private async Task<GetShoppingCartByIdQueryResult> GetShoppingCartById()
        {
            //arrange
            var contract = new GetShoppingCartByIdQueryExternal { ShoppingCartId = 1 };

            //act
            var response = await _client.PostAsJsonAsync("ShoppingCarts/GetShoppingCartById", contract);

            //assert
            response.EnsureSuccessStatusCode();
            var result = await Utilities.GetResponseContent<GetShoppingCartByIdQueryResult>(response);
            return result;
        }
    }
}