using AutoMapper;
using FluentAssertions;
using ShopingCarts.Application.Features.Queries.GetShoppingCartById;
using ShopingCarts.Application.Features.Queries.GetShoppingCartById.Dtos;
using ShopingCarts.Persistance.Context;
using ShoppingCart.Tests.Common;
using System.Security.Principal;

namespace ShoppingCart.Tests.Features.Queries.GetShoppingCartById
{
    [Collection("QueryCollection")]
    public class GetShoppingCartByIdQueryHandlerTests
    {
        private readonly ShoppingCartContext _context;
        private readonly IMapper _mapper;

        public GetShoppingCartByIdQueryHandlerTests(QueryTestBase testBase)
        {
            _context = testBase.Context;
            _mapper = testBase.Mapper;
        }

        [Fact]
        public async Task ShouldReturnShoppingCart()
        {
            //arrange
            var handler = new GetShoppingCartByIdQueryHandler(_context, _mapper);
            var query = new GetShoppingCartByIdQuery()
            {
                ExternalContract = new GetShoppingCartByIdQueryExternal
                {
                    ShoppingCartId = 1
                }
            };
            //act
            var result = await handler.Handle(
                  query,
                  cancellationToken: CancellationToken.None);
            //assert
            result.ShoppingCartDto.Should().BeOfType<ShoppingCartDto>();
            result.ShoppingCartDto.ShoppingCartProducts.Should().HaveCount(1);
            result.ShoppingCartDto.Total.Should().Be(3000);
            result.ErrorDescription.Should().BeNull();
        }


        [Fact]
        public async Task ShouldReturnNullAndErrorMessage()
        {
            //arrange
            var handler = new GetShoppingCartByIdQueryHandler(_context, _mapper);
            var query = new GetShoppingCartByIdQuery()
            {
                ExternalContract = new GetShoppingCartByIdQueryExternal
                {
                    ShoppingCartId = 10
                }
            };
            //act
            var result = await handler.Handle(
                    query,
                    cancellationToken: CancellationToken.None);
            //assert
            result.ShoppingCartDto.Should().BeNull();
            result.ErrorDescription.Should().NotBeNull();
            result.ErrorDescription.Should().Be("Shopping cart identify by Id: 10 doesn't exist.");
        }
    }
}
