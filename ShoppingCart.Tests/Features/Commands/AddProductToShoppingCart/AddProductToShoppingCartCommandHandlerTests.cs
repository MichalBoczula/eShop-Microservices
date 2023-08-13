using AutoMapper;
using FluentAssertions;
using Integrations.Products.Results;
using Moq;
using ShopingCarts.Application.Features.Commands.AddProductToShoppingCart;
using ShopingCarts.ExternalServices.SynchComunication.HttpClients.Abstract;
using ShopingCarts.Persistance.Context;
using ShoppingCart.Tests.Common;

namespace ShoppingCart.Tests.Features.Commands.AddProductToShoppingCart
{
    [Collection("AddCommandCollection")]
    public class AddProductToShoppingCartCommandHandlerTests
    {
        private readonly ShoppingCartContext _context;
        private readonly IMapper _mapper;
        private readonly Mock<IProductHttpService> _productHttpService;

        public AddProductToShoppingCartCommandHandlerTests(CommandTestBase testBase)
        {
            _context = testBase.Context;
            _mapper = testBase.Mapper;
            _productHttpService = testBase.ProductHttpService;
        }

        [Fact]
        public async Task ShouldAddAnotherToExistingProduct()
        {
            //arrange
            var handler = new AddProductToShoppingCartCommandHandler(_context, _mapper, _productHttpService.Object);
            var query = new AddProductToShoppingCartCommand()
            {
                ExternalContract = new AddProductToShoppingCartCommandExternal
                {
                    ShoppingCartId = 1,
                    ShoppingCartProductId = 1,
                    ShoppingCartProductQuantity = 1
                }
            };
            //act
            var result = await handler.Handle(
                  query,
                  cancellationToken: CancellationToken.None);
            //assert
            result.Should().BeOfType<AddProductToShoppingCartCommandResult>();
            result.PositiveMessage.Should().NotBeNull();
            result.PositiveMessage.Should().Be("Updated product with Id 1");
            result.ErrorDescription.Should().BeNull();
        }

        [Fact]
        public async Task ShouldReturnErrorProductDoesNotExistinShoppingCart()
        {
            //arrange
            var handler = new AddProductToShoppingCartCommandHandler(_context, _mapper, _productHttpService.Object);
            var query = new AddProductToShoppingCartCommand()
            {
                ExternalContract = new AddProductToShoppingCartCommandExternal
                {
                    ShoppingCartId = 1,
                    ShoppingCartProductId = 10,
                    ShoppingCartProductQuantity = 1
                }
            };
            //act
            var result = await handler.Handle(
                  query,
                  cancellationToken: CancellationToken.None);
            //assert
            result.Should().BeOfType<AddProductToShoppingCartCommandResult>();
            result.PositiveMessage.Should().BeNull();
            result.ErrorDescription.Should().NotBeNull();
            result.ErrorDescription.Should().Be("Product doesn't exist");
        }

        [Fact]
        public async Task ShouldAddNewProductToShoppingCart()
        {
            //arrange
            var integrationId = Guid.NewGuid();
            var integrationIds = new List<Guid>() { integrationId };
            var products = new List<ProductDto>()
            {
                new ProductDto
                {
                    Id = 2,
                    Name = "test",
                    Price = 1000,
                    ImgName = "test"
                }
            };
            var handler = new AddProductToShoppingCartCommandHandler(_context, _mapper, _productHttpService.Object);
            var query = new AddProductToShoppingCartCommand()
            {
                ExternalContract = new AddProductToShoppingCartCommandExternal
                {
                    ShoppingCartId = 1,
                    ShoppingCartProductIntegrationId = integrationId,
                    ShoppingCartProductQuantity = 1
                }
            };
            _productHttpService.Setup(x => x.GetProductsByIntegratinoIds(integrationIds)).ReturnsAsync(products);
            //act
            var result = await handler.Handle(
                  query,
                  cancellationToken: CancellationToken.None);
            //assert
            result.Should().BeOfType<AddProductToShoppingCartCommandResult>();
            result.PositiveMessage.Should().NotBeNull();
            result.PositiveMessage.Should().Be("Added new product with Id 2");
            result.ErrorDescription.Should().BeNull();
        }

        [Fact]
        public async Task ShouldReturnErrorProductDoesNotExistInShop()
        {
            //arrange
            var integrationId = Guid.NewGuid();
            var integrationIds = new List<Guid>() { integrationId };
            var products = new List<ProductDto>();
            var handler = new AddProductToShoppingCartCommandHandler(_context, _mapper, _productHttpService.Object);
            var query = new AddProductToShoppingCartCommand()
            {
                ExternalContract = new AddProductToShoppingCartCommandExternal
                {
                    ShoppingCartId = 1,
                    ShoppingCartProductIntegrationId = integrationId,
                    ShoppingCartProductQuantity = 1
                }
            };
            _productHttpService.Setup(x => x.GetProductsByIntegratinoIds(integrationIds)).ReturnsAsync(products);
            //act
            var result = await handler.Handle(
                  query,
                  cancellationToken: CancellationToken.None);
            //assert
            result.Should().BeOfType<AddProductToShoppingCartCommandResult>();
            result.PositiveMessage.Should().BeNull();
            result.ErrorDescription.Should().NotBeNull();
            result.ErrorDescription.Should().Be("Product doesn't exist");
        }
    }
}
