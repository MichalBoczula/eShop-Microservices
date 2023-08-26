using AutoMapper;
using Integrations.Products.GetProductsByIntegrationId.Requests;
using Microsoft.Extensions.Configuration;
using Moq;
using NSubstitute;
using ShopingCarts.Application.Features.Queries.GetShoppingCartById;
using ShopingCarts.Application.Features.Queries.GetShoppingCartById.Dtos;
using ShopingCarts.ExternalServices.SynchComunication.HttpClients.Concrete.Products;
using ShopingCarts.ExternalServices.SynchComunication.HttpClients.Products.Abstract;
using ShopingCarts.Persistance.Context;
using ShoppingCart.Tests.Common;
using System.Diagnostics.Contracts;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Policy;

namespace ShoppingCart.Tests.ExternalServices.SynchComunication.Products
{
    [Collection("QueryCollection")]
    public class ProductHttpServiceTests
    {
        private Mock<IProductHttpService> _productHttpService;

        public ProductHttpServiceTests(QueryTestBase testBase)
        {
            _productHttpService = testBase.ProductHttpService;
        }

        [Fact]
        public async Task ShouldReturnShoppingCart()
        {
            //arrange
            Assert.Fail("");
        }
    }
}
