using AutoMapper;
using FluentAssertions;
using Integrations.Products.GetProductsByIntegrationId.Results;
using Integrations.ShoppingCart;
using Moq;
using ShopingCarts.Application.Features.Commands.UpdateShoppingCart;
using ShopingCarts.ExternalServices.SynchComunication.HttpClients.Products.Abstract;
using ShopingCarts.Persistance.Context;
using ShoppingCart.Tests.Common;

namespace ShoppingCart.Tests.Features.Commands.UpdateShoppingCart
{
    [Collection("UpdateCommandCollection")]
    public class UpdateShoppingCartCommandHandlerTests
    {
        private readonly ShoppingCartContext _context;
        private readonly IMapper _mapper;
        private readonly Mock<IProductsHttpRequestHandler> _productsHttpRequestHandler;

        public UpdateShoppingCartCommandHandlerTests(CommandTestBase testBase)
        {
            _context = testBase.Context;
            _mapper = testBase.Mapper;
        }

        [Fact]
        public async Task ShouldRetrunErrorMessage()
        {
            //arrange
            var handler = new UpdateShoppingCartCommandHandler(_context, _mapper, _productsHttpRequestHandler.Object);
            var query = new UpdateShoppingCartCommand()
            {
                ShoppingCartIntegrationId = new Guid("D0E9939D-0000-0000-0000-DB076F6C5278"),
                Products = new List<ShoppingCartProductExternal> {
                        new ShoppingCartProductExternal()
                        {
                            Id = 1,
                            Quantity = 2,
                        }
                    }
            };
            _productsHttpRequestHandler.Setup(x => x.GetProductsByIntegrationIds(It.IsAny<List<Guid>>()))
                .ReturnsAsync(new GetProductsByIntegrationIdQueryResult
                {
                    Products = new List<ProductExternal>()
                    {
                        new ProductExternal()
                            {
                                Id = 1
                            }
                    }
                });
            //act
            var result = await handler.Handle(
                  query,
                  cancellationToken: CancellationToken.None);
            //assert
            result.Should().BeOfType<UpdateShoppingCartCommandResult>();
            result.PositiveMessage.Should().BeNull();
            result.ErrorDescription.Should().NotBeNull();
            result.ErrorDescription.Should().Be($"Shopping cart identify by integrationId d0e9939d-0000-0000-0000-db076f6c5278 doesn't exist");
        }
    }
}
