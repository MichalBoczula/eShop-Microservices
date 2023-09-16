using AutoMapper;
using FluentAssertions;
using Moq;
using ShopingCarts.Application.Features.Commands.AddProductToShoppingCart;
using ShopingCarts.Application.Features.Commands.RemoveProductFromShoppingCart;
using ShopingCarts.Application.Features.Queries.GetShoppingCartById;
using ShopingCarts.ExternalServices.SynchComunication.HttpClients.Concrete.Products;
using ShopingCarts.ExternalServices.SynchComunication.HttpClients.Products.Abstract;
using ShopingCarts.Persistance.Context;
using ShoppingCart.Tests.Common;

namespace ShoppingCart.Tests.Features.Commands.RemoveProductFromShoppingCart
{
    [Collection("RemoveCommandCollection")]
    public class RemoveProductFromShoppingCartCommandHandlerTests
    {
        private readonly ShoppingCartContext _context;
        private readonly IMapper _mapper;
        private readonly Mock<IProductsHttpRequestHandler> _productsHttpRequestHandler;

        public RemoveProductFromShoppingCartCommandHandlerTests(CommandTestBase testBase)
        {
            _context = testBase.Context;
            _mapper = testBase.Mapper;
            _productsHttpRequestHandler = testBase.ProductsHttpRequestHandler;
        }

        [Fact]
        public async Task ShouldRemoveProductFromShoppingCart()
        {
            //arrange
            var handler = new RemoveProductFromShoppingCartCommandHandler(_context, _mapper);
            var query = new RemoveProductFromShoppingCartCommand()
            {
                ShoppingCartId = 1,
                ShoppingCartProductId = 1,
            };
            var getShoppingCartByIdQueryHandler = new GetShoppingCartByIdQueryHandler(_context, _mapper);
            var getShoppingCartByIdQuery = new GetShoppingCartByIdQuery()
            {
                ExternalContract = new GetShoppingCartByIdQueryExternal
                {
                    ShoppingCartId = 1
                }
            };
            var addProductToShoppingCartCommandHandler = new AddProductToShoppingCartCommandHandler(_context, _mapper, _productsHttpRequestHandler.Object);
            var addProductToShoppingCartCommand = new AddProductToShoppingCartCommand()
            {
                ShoppingCartId = 1,
                ExternalContract = new AddProductToShoppingCartCommandExternal
                {
                    ShoppingCartProductId = 1,
                    ShoppingCartProductQuantity = 1
                }
            };
            await addProductToShoppingCartCommandHandler.Handle(addProductToShoppingCartCommand, CancellationToken.None);
            //act
            var result = await handler.Handle(
                  query,
                  cancellationToken: CancellationToken.None);
            //assert
            result.Should().BeOfType<RemoveProductFromShoppingCartCommandResult>();
            result.PositiveMessage.Should().NotBeNull();
            result.PositiveMessage.Should().Be("Successfullyy removed with id 1 product from shoppingCart identify by 1");
            result.ErrorDescription.Should().BeNull();
            var shoppingCart = await getShoppingCartByIdQueryHandler.Handle(getShoppingCartByIdQuery, CancellationToken.None);
            shoppingCart.ShoppingCartDto.ShoppingCartProducts.Should().HaveCount(1);
            shoppingCart.ShoppingCartDto.Total.Should().Be(3000);

        }

        [Fact]
        public async Task ShouldReturnAnErrorShoppingCartDoesNotExist()
        {
            //arrange
            var handler = new RemoveProductFromShoppingCartCommandHandler(_context, _mapper);
            var query = new RemoveProductFromShoppingCartCommand()
            {
                ShoppingCartId = 111,
                ShoppingCartProductId = 1,
            };
            //act
            var result = await handler.Handle(
                  query,
                  cancellationToken: CancellationToken.None);
            //assert
            result.Should().BeOfType<RemoveProductFromShoppingCartCommandResult>();
            result.PositiveMessage.Should().BeNull();
            result.ErrorDescription.Should().NotBeNull();
            result.ErrorDescription.Should().Be("Shopping cart identify by id 111 doesn't exist");
        }

        [Fact]
        public async Task ShouldReturnErrorMessageProductDoesNotExistInShoppingCart()
        {
            //arrange
            var handler = new RemoveProductFromShoppingCartCommandHandler(_context, _mapper);
            var query = new RemoveProductFromShoppingCartCommand()
            {
                ShoppingCartId = 1,
                ShoppingCartProductId = 10,
            };
            //act
            var result = await handler.Handle(
                  query,
                  cancellationToken: CancellationToken.None);
            //assert
            result.Should().BeOfType<RemoveProductFromShoppingCartCommandResult>();
            result.PositiveMessage.Should().BeNull();
            result.ErrorDescription.Should().NotBeNull();
            result.ErrorDescription.Should().Be("Product identify by id 10 doesn't exist in shoppingCart identify by 1");
        }

        [Fact]
        public async Task ShouldSubstractQuantityFromProduct()
        {
            // arrange
            var handler = new RemoveProductFromShoppingCartCommandHandler(_context, _mapper);
            var query = new RemoveProductFromShoppingCartCommand()
            {
                ShoppingCartId = 1,
                ShoppingCartProductId = 1,
            };
            var getShoppingCartByIdQueryHandler = new GetShoppingCartByIdQueryHandler(_context, _mapper);
            var getShoppingCartByIdQuery = new GetShoppingCartByIdQuery()
            {
                ExternalContract = new GetShoppingCartByIdQueryExternal
                {
                    ShoppingCartId = 1
                }
            };
            var addProductToShoppingCartCommandHandler = new AddProductToShoppingCartCommandHandler(_context, _mapper, _productsHttpRequestHandler.Object);
            var addProductToShoppingCartCommand = new AddProductToShoppingCartCommand()
            {
                ShoppingCartId = 1,
                ExternalContract = new AddProductToShoppingCartCommandExternal
                {
                    ShoppingCartProductId = 1,
                    ShoppingCartProductQuantity = 1
                }
            };
            await addProductToShoppingCartCommandHandler.Handle(addProductToShoppingCartCommand, CancellationToken.None);
            //act
            var result = await handler.Handle(
                  query,
                  cancellationToken: CancellationToken.None);
            //assert
            result.Should().BeOfType<RemoveProductFromShoppingCartCommandResult>();
            result.ErrorDescription.Should().BeNull();
            result.PositiveMessage.Should().NotBeNull();
            result.PositiveMessage.Should().Be("Successfullyy removed with id 1 product from shoppingCart identify by 1");
            var shoppingCart = await getShoppingCartByIdQueryHandler.Handle(getShoppingCartByIdQuery, CancellationToken.None);
            shoppingCart.ShoppingCartDto.ShoppingCartProducts.Should().HaveCount(1);
            shoppingCart.ShoppingCartDto.ShoppingCartProducts.First().Quantity.Should().Be(1);
            shoppingCart.ShoppingCartDto.Total.Should().Be(3000);
        }
    }
}
