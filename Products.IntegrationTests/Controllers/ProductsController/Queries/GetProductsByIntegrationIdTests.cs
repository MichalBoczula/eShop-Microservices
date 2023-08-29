using FluentAssertions;
using Integrations.Common;
using Integrations.Products.GetProductsByIntegrationId.Requests;
using Integrations.Products.GetProductsByIntegrationId.Results;
using Products.IntegrationTests.Configuration;
using System.Net.Http.Json;

namespace Products.IntegrationTests.Controllers.ProductsController.Queries
{
    public class GetProductsByIntegrationIdTests : IClassFixture<ProductsWebAppFactory<Program>>
    {
        private readonly HttpClient _client;

        public GetProductsByIntegrationIdTests(ProductsWebAppFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ShouldReturnList()
        {
            //arrange
            var integrationIds = new List<Guid>
            {
                new Guid("55CCEE28-E15D-4644-A7BE-2F8A93568D6F"),
                new Guid("0EF1268E-33D6-49CD-A4B5-8EB94494D896"),
                new Guid("23363AFF-DD71-4F3C-8381-F7E71021761E")
            };
            var contract = new GetProductsByIntegrationIdQueryExternal { IntegrationIds = integrationIds };

            //act
            var response = await _client.PostAsJsonAsync("Products/GetProductsByIntegrationId", contract);

            //assert
            response.EnsureSuccessStatusCode();
            var result = await Utilities.GetResponseContent<GetProductsByIntegrationIdQueryResult>(response);
            result.Products.Should().HaveCount(3);
            result.ErrorDescription.Should().BeNull();
        }

        [Fact]
        public async Task ShouldReturnEmptyList()
        {
            //arrange
            var integrationIds = new List<Guid>
            {
                new Guid("55CCEE28-E15D-4644-A7BE-2F8A93568D6A"),
            };
            var contract = new GetProductsByIntegrationIdQueryExternal { IntegrationIds = integrationIds };

            //act
            var response = await _client.PostAsJsonAsync("Products/GetProductsByIntegrationId", contract);

            //assert
            response.EnsureSuccessStatusCode();
            var result = await Utilities.GetResponseContent<GetProductsByIntegrationIdQueryResult>(response);
            result.Products.Should().HaveCount(0);
            result.ErrorDescription.Should().BeNull();
        }
    }
}
