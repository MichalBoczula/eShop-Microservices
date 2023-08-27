using FluentAssertions;
using Integrations.Products.GetProductsByIntegrationId.Requests;
using Integrations.Products.GetProductsByIntegrationId.Results;
using Microsoft.Extensions.Configuration;
using Moq;
using ShopingCarts.ExternalServices.SynchComunication.HttpClients.Concrete.Products;
using ShopingCarts.ExternalServices.SynchComunication.HttpClients.Products.Abstract;
using ShoppingCart.Tests.Common;

namespace ShoppingCart.Tests.ExternalServices.SynchComunication.Products
{
    [Collection("QueryCollection")]
    public class ProductsHttpRequestHandlerTests
    {
        private Mock<IProductHttpService> _productHttpService;
        private Mock<IConfiguration> _configuration;

        public ProductsHttpRequestHandlerTests(QueryTestBase testBase)
        {
            _productHttpService = testBase.ProductHttpService;
            _configuration = testBase.Configuration;
        }

        [Fact]
        public async Task ShouldReturnList()
        {
            //arrange
            this._productHttpService.Setup(x =>
                x.GetProductsByIntegrationId(It.IsAny<string>(), It.IsAny<GetProductsByIntegrationIdQueryExternal>()))
                .ReturnsAsync(new GetProductsByIntegrationIdQueryResult()
                {
                    Products = new List<ProductExternal>
                    {
                        new ProductExternal
                        {
                            Id = 1,
                            Name = "test",
                            Price = 1000
                        }
                    }
                });
            _configuration.SetupGet(x => x[It.IsAny<string>()]).Returns("test");

            var productsHttpRequestHandler = new ProductsHttpRequestHandler(this._productHttpService.Object, this._configuration.Object);

            //act 
            var result = await productsHttpRequestHandler.GetProductsByIntegrationIds(new List<Guid>());

            //assert
            result.Products.Should().HaveCount(1);
            result.ErrorDescription.Should().BeNull();
        }

        [Fact]
        public async Task ShouldReturnErrorDescription()
        {
            //arrange
            this._productHttpService.Setup(x =>
                x.GetProductsByIntegrationId(It.IsAny<string>(), It.IsAny<GetProductsByIntegrationIdQueryExternal>()))
                .ReturnsAsync(new GetProductsByIntegrationIdQueryResult()
                {
                    ErrorDescription = "test"
                });
            _configuration.SetupGet(x => x[It.IsAny<string>()]).Returns("test");

            var productsHttpRequestHandler = new ProductsHttpRequestHandler(this._productHttpService.Object, this._configuration.Object);

            //act 
            var result = await productsHttpRequestHandler.GetProductsByIntegrationIds(new List<Guid>());

            //assert
            result.Products.Should().BeNull();
            result.ErrorDescription.Should().Be("test");
        }

        [Fact]
        public async Task ShouldThrowException()
        {
            //arrange
            this._productHttpService.Setup(x =>
                x.GetProductsByIntegrationId(It.IsAny<string>(), It.IsAny<GetProductsByIntegrationIdQueryExternal>()))
                .ThrowsAsync(new Exception("test"));

            _configuration.SetupGet(x => x[It.IsAny<string>()]).Returns("test");

            var productsHttpRequestHandler = new ProductsHttpRequestHandler(this._productHttpService.Object, this._configuration.Object);

            //act 
            Func<Task> result = () => productsHttpRequestHandler.GetProductsByIntegrationIds(new List<Guid>());

            //assert
            await result.Should().ThrowAsync<Exception>();
        }
    }
}
