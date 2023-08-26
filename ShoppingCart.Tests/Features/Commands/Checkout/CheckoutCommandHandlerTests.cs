using AutoMapper;
using FluentAssertions;
using Integrations.Orders.Request;
using Integrations.Orders.Results;
using Moq;
using ShopingCarts.Application.Features.Commands.Checkout;
using ShopingCarts.Application.Features.Commands.CreateShoppingCart;
using ShopingCarts.Application.Features.Queries.GetShoppingCartById;
using ShopingCarts.ExternalServices.SynchComunication.HttpClients.Orders.Abstract;
using ShopingCarts.Persistance.Context;
using ShoppingCart.Tests.Common;

namespace ShoppingCart.Tests.Features.Commands.Checkout
{
    [Collection("CheckoutCommandCollection")]
    public class CheckoutCommandHandlerTests
    {
        private readonly ShoppingCartContext _context;
        private readonly IMapper _mapper;
        private readonly Mock<IOrderHttpService> _orderHttpService;

        public CheckoutCommandHandlerTests(CommandTestBase testBase)
        {
            _context = testBase.Context;
            _mapper = testBase.Mapper;
            _orderHttpService = testBase.OrderHttpService;
        }

        [Fact]
        public async Task ShouldCreateOrder()
        {
            //arrange
            var handler = new CheckoutCommandHandler(_context, _mapper, _orderHttpService.Object);
            var query = new CheckoutCommand()
            {
                ExternalContract = new CheckoutCommandExternal
                {
                    ShoppingCartId = 1
                }
            };
            var getShoppingCartByIdQueryHandler = new GetShoppingCartByIdQueryHandler(_context, _mapper);
            var getShoppingCartByIdQuery = new GetShoppingCartByIdQuery()
            {
                ExternalContract = new GetShoppingCartByIdQueryExternal
                {
                    ShoppingCartId = 1
                }
            };
            var externalResponse = new CreateOrderDto()
            {
                PositiveMessage = "New order identify by id 1 has been created",
                ErrorDescription = null
            };
            _orderHttpService.Setup(x => x.CreateOrder(It.IsAny<ShoppingCartExternal>())).ReturnsAsync(externalResponse);
            //act
            var result = await handler.Handle(
                  query,
                  cancellationToken: CancellationToken.None);
            //assert
            result.Should().BeOfType<CheckoutCommandResult>();
            result.PositiveMessage.Should().NotBeNull();
            result.PositiveMessage.Should().Be("New order identify by id 1 has been created");
            result.ErrorDescription.Should().BeNull();
            var shoppingCart = await getShoppingCartByIdQueryHandler.Handle(getShoppingCartByIdQuery, CancellationToken.None);
            shoppingCart.ShoppingCartDto.Should().BeNull();
        }

        [Fact]
        public async Task ShouldReturnErrorShoppingCartDoesNotExist()
        {
            //arrange
            var handler = new CheckoutCommandHandler(_context, _mapper, _orderHttpService.Object);
            var query = new CheckoutCommand()
            {
                ExternalContract = new CheckoutCommandExternal
                {
                    ShoppingCartId = 111
                }
            };
            var getShoppingCartByIdQueryHandler = new GetShoppingCartByIdQueryHandler(_context, _mapper);
            var getShoppingCartByIdQuery = new GetShoppingCartByIdQuery()
            {
                ExternalContract = new GetShoppingCartByIdQueryExternal
                {
                    ShoppingCartId = 111
                }
            };
            var externalResponse = new CreateOrderDto()
            {
                PositiveMessage = "New order identify by id 1 has been created",
                ErrorDescription = null
            };
            _orderHttpService.Setup(x => x.CreateOrder(It.IsAny<ShoppingCartExternal>())).ReturnsAsync(externalResponse);
            //act
            var result = await handler.Handle(
                  query,
                  cancellationToken: CancellationToken.None);
            //assert
            result.Should().BeOfType<CheckoutCommandResult>();
            result.ErrorDescription.Should().NotBeNull();
            result.ErrorDescription.Should().Be("Shopping cart identify by id 111 doesn't exist");
            result.PositiveMessage.Should().BeNull();
            var shoppingCart = await getShoppingCartByIdQueryHandler.Handle(getShoppingCartByIdQuery, CancellationToken.None);
            shoppingCart.ShoppingCartDto.Should().BeNull();
        }

        [Fact]
        public async Task ShouldReturnErrorThereWasProblemWithCreatingOrder()
        {
            //arrange
            var handler = new CheckoutCommandHandler(_context, _mapper, _orderHttpService.Object);
            var query = new CheckoutCommand()
            {
                ExternalContract = new CheckoutCommandExternal
                {
                    ShoppingCartId = 1
                }
            };
            var getShoppingCartByIdQueryHandler = new GetShoppingCartByIdQueryHandler(_context, _mapper);
            var getShoppingCartByIdQuery = new GetShoppingCartByIdQuery()
            {
                ExternalContract = new GetShoppingCartByIdQueryExternal
                {
                    ShoppingCartId = 1
                }
            };
            var externalResponse = new CreateOrderDto()
            {
                PositiveMessage = null,
                ErrorDescription = "Error occured during creating order process for shopping cart identify by 1-1-1"
            };
            _orderHttpService.Setup(x => x.CreateOrder(It.IsAny<ShoppingCartExternal>())).ReturnsAsync(externalResponse);
            //act
            var result = await handler.Handle(
                  query,
                  cancellationToken: CancellationToken.None);
            //assert
            result.Should().BeOfType<CheckoutCommandResult>();
            result.ErrorDescription.Should().NotBeNull();
            result.ErrorDescription.Should().Be("Error occured during creating order process for shopping cart identify by 1-1-1");
            result.PositiveMessage.Should().BeNull();
            var shoppingCart = await getShoppingCartByIdQueryHandler.Handle(getShoppingCartByIdQuery, CancellationToken.None);
            shoppingCart.ShoppingCartDto.Should().NotBeNull();
        }
    }
}