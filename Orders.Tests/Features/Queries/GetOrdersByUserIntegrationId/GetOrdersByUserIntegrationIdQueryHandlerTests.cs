using AutoMapper;
using FluentAssertions;
using Orders.Application.Features.Queries.GetOrdersByUserIntegrationId;
using Orders.Persistance.Context;
using Orders.Tests.Common;

namespace Orders.Tests.Features.Queries.GetOrdersByUserIntegrationId
{
    [Collection("QueryCollection")]
    public class GetOrdersByUserIntegrationIdQueryHandlerTests
    {
        private readonly OrderContext _context;
        private readonly IMapper _mapper;

        public GetOrdersByUserIntegrationIdQueryHandlerTests(QueryTestBase testBase)
        {
            _context = testBase.Context;
            _mapper = testBase.Mapper;
        }

        [Fact]
        public async Task ShouldReturnList()
        {
            //arrange
            var handler = new GetOrdersByUserIntegrationIdQueryHandler(_context, _mapper);
            //act
            var result = await handler.Handle(
                    new GetOrdersByUserIntegrationIdQuery()
                    {
                        ExternalContract = new GetOrdersByUserIntegrationIdQueryExternal
                        {
                            UserIntegrationId = new Guid("95464765-CF3F-4ED7-B353-5D2F810DCC33"),
                        }
                    },
                    cancellationToken: CancellationToken.None);
            //assert
            result.Should().BeOfType<GetOrdersByUserIntegrationIdQueryResult>();
            result.Orders.Should().HaveCount(1);
            result.Orders.First().OrderProducts.Should().HaveCount(1);
            result.ErrorDescription.Should().BeNull();
        }

        [Fact]
        public async Task ShouldReturnEmptyList()
        {
            //arrange
            var handler = new GetOrdersByUserIntegrationIdQueryHandler(_context, _mapper);
            //act
            var result = await handler.Handle(
                    new GetOrdersByUserIntegrationIdQuery()
                    {
                        ExternalContract = new GetOrdersByUserIntegrationIdQueryExternal
                        {
                            UserIntegrationId = new Guid("95464765-CF3F-4ED7-B353-5D2F810DCC44"),
                        }
                    },
                    cancellationToken: CancellationToken.None);
            //assert
            result.Should().BeOfType<GetOrdersByUserIntegrationIdQueryResult>();
            result.Orders.Should().BeNull();
            result.ErrorDescription.Should().Be($"Orders for user identify by 95464765-cf3f-4ed7-b353-5d2f810dcc44 doesn't exist");
        }
    }
}
