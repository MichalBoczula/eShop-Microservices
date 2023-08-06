using AutoMapper;
using FluentAssertions;
using Products.Application.Features.Queries.GetProductsByIntegrationId;
using Products.Persistance.Context;
using Products.Tests.Common;

namespace Products.Tests.Features.Queries.GetProductsByIntegrationId
{
    [Collection("QueryCollection")]
    public class GetProductsByIntegrationIdQueryHandlerTests
    {
        private readonly ProductsContext _context;
        private readonly IMapper _mapper;

        public GetProductsByIntegrationIdQueryHandlerTests(QueryTestBase testBase)
        {
            _context = testBase.Context;
            _mapper = testBase.Mapper;
        }

        [Fact]
        public async Task ShouldReturnList()
        {
            //arrange
            var handler = new GetProductsByIntegrationIdQueryHandler(_context, _mapper);
            //act
            var result = await handler.Handle(
                    new GetProductsByIntegrationIdQuery()
                    {
                        ExternalContract = new GetProductsByIntegrationIdQueryExternal
                        {
                            IntegrationIds = new List<Guid>
                            {
                                new Guid("55ccee28-e15d-4644-a7be-2f8a93568d6f"),
                                new Guid("0ef1268e-33d6-49cd-a4b5-8eb94494d896"),
                                new Guid("23363aff-dd71-4f3c-8381-f7e71021761e")
                            }
                        }
                    },
                    cancellationToken: CancellationToken.None);
            //assert
            result.Products.Should().HaveCount(3);
            result.ErrorDescription.Should().BeNull();
        }

        [Fact]
        public async Task ShouldReturnEmptyList()
        {
            //arrange
            var handler = new GetProductsByIntegrationIdQueryHandler(_context, _mapper);
            //act
            var result = await handler.Handle(
                    new GetProductsByIntegrationIdQuery()
                    {
                        ExternalContract = new GetProductsByIntegrationIdQueryExternal
                        {
                            IntegrationIds = new List<Guid>
                            {
                                new Guid("23363aff-dd71-4f3c-8381-f7e71021761a"),
                            }
                        }
                    },
                    cancellationToken: CancellationToken.None);
            //assert
            result.Products.Should().HaveCount(0);
            result.ErrorDescription.Should().BeNull();
        }
    }
}
