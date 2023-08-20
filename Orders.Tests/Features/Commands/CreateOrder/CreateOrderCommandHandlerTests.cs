using AutoMapper;
using FluentAssertions;
using Integrations.Orders.Request;
using Integrations.Orders.Results;
using Moq;
using Orders.Application.Features.Commands.CreateOrder;
using Orders.Application.Features.Queries.GetOrdersByUserIntegrationId;
using Orders.Persistance.Context;
using Orders.Tests.Common;

namespace Orders.Tests.Features.Commands.CreateOrder
{
    [Collection("CreateOrderCommandCollection")]
    public class CreateOrderCommandHandlerTests
    {
        private readonly OrderContext _context;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandlerTests(CommandTestBase testBase)
        {
            _context = testBase.Context;
            _mapper = testBase.Mapper;
        }

        [Fact]
        public async Task ShouldReturnErrorUserDoesNotExist()
        {
            //arrange
            var handler = new CreateOrderCommandHandler(_context, _mapper);
            var query = new CreateOrderCommand()
            {
                ExternalContract = new CreateOrderCommandExternal
                {
                    ShoppingCart = new ShoppingCartExternal()
                    {
                        Products = new List<ShoppingCartProductExternal>(),
                        Total = 2000,
                        UserIntegrationId = new Guid("95464765-CF3F-4ED7-B353-5D2F810DCC44")
                    }
                }
            };
            //act
            var result = await handler.Handle(
                  query,
                  cancellationToken: CancellationToken.None);
            //assert
            result.Should().BeOfType<CreateOrderCommandResult>();
            result.PositiveMessage.Should().BeNull();
            result.ErrorDescription.Should().Be("User identify by 95464765-cf3f-4ed7-b353-5d2f810dcc44 doesn't exist");
        }

        [Fact]
        public async Task ShouldCreateOrder()
        {
            //arrange
            var handler = new CreateOrderCommandHandler(_context, _mapper);
            var query = new CreateOrderCommand()
            {
                ExternalContract = new CreateOrderCommandExternal
                {
                    ShoppingCart = new ShoppingCartExternal()
                    {
                        Products = new List<ShoppingCartProductExternal>(),
                        Total = 2000,
                        UserIntegrationId = new Guid("95464765-CF3F-4ED7-B353-5D2F810DCC33"),
                        ShoppingCartIntegrationId = new Guid("95464765-CF3F-4ED7-B353-5D2F810DCC44")
                    }
                }
            };
            //act
            var result = await handler.Handle(
                  query,
                  cancellationToken: CancellationToken.None);
            //assert

            var getOrderQueryHandler = new GetOrdersByUserIntegrationIdQueryHandler(_context, _mapper);
            var getOrderQueryResult = await getOrderQueryHandler.Handle(
                    new GetOrdersByUserIntegrationIdQuery()
                    {
                        ExternalContract = new GetOrdersByUserIntegrationIdQueryExternal
                        {
                            UserIntegrationId = new Guid("95464765-CF3F-4ED7-B353-5D2F810DCC33"),
                        }
                    },
                    cancellationToken: CancellationToken.None);
            getOrderQueryResult.Orders.Should().HaveCount(2);
            result.Should().BeOfType<CreateOrderCommandResult>();
            result.PositiveMessage.Should().Be("Order identify by integration Id 95464765-cf3f-4ed7-b353-5d2f810dcc44 has been created");
            result.ErrorDescription.Should().BeNull();
        }
    }
}
