using AutoMapper;
using FluentAssertions;
using Products.Application.Features.Queries.GetAllProducts;
using Products.Persistance.Context;
using Products.Tests.Common;

namespace Products.Tests.Features.Queries.GetAllProducts
{
    [Collection("QueryCollection")]
    public class GetAllProductsQueryHandlerTests
    {
        private readonly ProductsContext _context;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandlerTests(QueryTestBase testBase)
        {
            _context = testBase.Context;
            _mapper = testBase.Mapper;
        }

        [Fact]
        public async Task ShouldReturnList()
        {
            //arrange
            var handler = new GetAllProductsQueryHandler(_context, _mapper);
            //act
            var result = await handler.Handle(
                    new GetAllProductsQuery()
                    {
                    },
                    cancellationToken: CancellationToken.None);
            //assert
            result.Products.Should().HaveCount(3);
            result.ErrorDescription.Should().BeNull();
        }
    }
}