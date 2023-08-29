using FluentAssertions;
using Integrations.Common;
using Products.Application.Features.Queries.GetAllProducts;
using Products.IntegrationTests.Configuration;

namespace Products.IntegrationTests.Controllers.ProductsController.Queries
{
    public class GetProductsTests : IClassFixture<ProductsWebAppFactory<Program>>
    {
        private readonly HttpClient _client;

        public GetProductsTests(ProductsWebAppFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ShouldReturnList()
        {
            //arrange & act
            var response = await _client.GetAsync("Products/GetProducts");

            //assert
            response.EnsureSuccessStatusCode();
            var result = await Utilities.GetResponseContent<GetAllProductsQueryResult>(response);
            result.Products.Should().HaveCount(3);
            result.ErrorDescription.Should().BeNull();
        }
    }
}
