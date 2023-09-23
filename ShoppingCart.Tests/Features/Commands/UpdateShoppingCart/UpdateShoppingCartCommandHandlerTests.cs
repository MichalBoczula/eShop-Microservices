using AutoMapper;
using FluentAssertions;
using Integrations.Products.GetProductsByIntegrationId.Results;
using Integrations.ShoppingCart;
using Moq;
using ShopingCarts.Application.Features.Commands.UpdateShoppingCart;
using ShopingCarts.Application.Features.Queries.GetShoppingCartById;
using ShopingCarts.Application.Features.Queries.GetShoppingCartById.Dtos;
using ShopingCarts.Domain.Entities;
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
            _productsHttpRequestHandler = testBase.ProductsHttpRequestHandler;
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

        [Fact]
        public async Task ShouldRemoveProductFromShoppingCart()
        {
            //arrange
            await this.SetUpShoppingCart1();
            var handler = new UpdateShoppingCartCommandHandler(_context, _mapper, _productsHttpRequestHandler.Object);
            var query = new UpdateShoppingCartCommand()
            {
                ShoppingCartIntegrationId = new Guid("0EF1268E-CCCC-DDDD-EEEE-8EB94494D896"),
                Products = new List<ShoppingCartProductExternal> {
                        new ShoppingCartProductExternal()
                        {
                            Id = 5,
                            Quantity = 0,
                        }
                    }
            };
            //act
            var result = await handler.Handle(
                  query,
                  cancellationToken: CancellationToken.None);
            //assert
            result.Should().BeOfType<UpdateShoppingCartCommandResult>();
            result.ErrorDescription.Should().BeNull();
            result.PositiveMessage.Should().Be($"Successfully updated shopping cart identify by integrationId 0ef1268e-cccc-dddd-eeee-8eb94494d896");


            var handlerGet = new GetShoppingCartByIdQueryHandler(_context, _mapper);
            var queryGet = new GetShoppingCartByIdQuery()
            {
                ExternalContract = new GetShoppingCartByIdQueryExternal
                {
                    ShoppingCartId = 2
                }
            };
            var resultGet = await handlerGet.Handle(
                  queryGet,
                  cancellationToken: CancellationToken.None);
            resultGet.ShoppingCartDto.Should().BeOfType<ShoppingCartDto>();
            resultGet.ShoppingCartDto.ShoppingCartProducts.Should().BeEmpty();
            resultGet.ShoppingCartDto.Total.Should().Be(0);
        }

        [Fact]
        public async Task ShouldRemoveAllProductsFromShoppingCart()
        {
            //arrange
            await this.SetUpShoppingCart2();
            var handler = new UpdateShoppingCartCommandHandler(_context, _mapper, _productsHttpRequestHandler.Object);
            var query = new UpdateShoppingCartCommand()
            {
                ShoppingCartIntegrationId = new Guid("0EF1268E-FFFF-FFFF-FFFF-8EB94494D896"),
                Products = new List<ShoppingCartProductExternal> {
                        new ShoppingCartProductExternal()
                        {
                            Id = 6,
                            Quantity = 0,
                        },
                        new ShoppingCartProductExternal()
                        {
                            Id = 7,
                            Quantity = 0,
                        }
                    }
            };
            //act
            var result = await handler.Handle(
                  query,
                  cancellationToken: CancellationToken.None);
            //assert
            result.Should().BeOfType<UpdateShoppingCartCommandResult>();
            result.ErrorDescription.Should().BeNull();
            result.PositiveMessage.Should().Be($"Successfully updated shopping cart identify by integrationId 0ef1268e-ffff-ffff-ffff-8eb94494d896");


            var handlerGet = new GetShoppingCartByIdQueryHandler(_context, _mapper);
            var queryGet = new GetShoppingCartByIdQuery()
            {
                ExternalContract = new GetShoppingCartByIdQueryExternal
                {
                    ShoppingCartId = 3
                }
            };
            var resultGet = await handlerGet.Handle(
                  queryGet,
                  cancellationToken: CancellationToken.None);
            resultGet.ShoppingCartDto.Should().BeOfType<ShoppingCartDto>();
            resultGet.ShoppingCartDto.ShoppingCartProducts.Should().BeEmpty();
            resultGet.ShoppingCartDto.Total.Should().Be(0);
        }

        [Fact]
        public async Task ShouldRemoveOnlyOneProductFromShoppingCart()
        {
            //arrange
            await this.SetUpShoppingCart3();
            var handler = new UpdateShoppingCartCommandHandler(_context, _mapper, _productsHttpRequestHandler.Object);
            var query = new UpdateShoppingCartCommand()
            {
                ShoppingCartIntegrationId = new Guid("0EF1268E-CCCC-CCCC-CCCC-8EB94494D896"),
                Products = new List<ShoppingCartProductExternal> {
                        new ShoppingCartProductExternal()
                        {
                            Id = 9,
                            Quantity = 0,
                        }
                    }
            };
            //act
            var result = await handler.Handle(
                  query,
                  cancellationToken: CancellationToken.None);
            //assert
            result.Should().BeOfType<UpdateShoppingCartCommandResult>();
            result.ErrorDescription.Should().BeNull();
            result.PositiveMessage.Should().Be($"Successfully updated shopping cart identify by integrationId 0ef1268e-cccc-cccc-cccc-8eb94494d896");


            var handlerGet = new GetShoppingCartByIdQueryHandler(_context, _mapper);
            var queryGet = new GetShoppingCartByIdQuery()
            {
                ExternalContract = new GetShoppingCartByIdQueryExternal
                {
                    ShoppingCartId = 4
                }
            };
            var resultGet = await handlerGet.Handle(
                  queryGet,
                  cancellationToken: CancellationToken.None);
            resultGet.ShoppingCartDto.Should().BeOfType<ShoppingCartDto>();
            resultGet.ShoppingCartDto.ShoppingCartProducts.Should().HaveCount(1);
            resultGet.ShoppingCartDto.Total.Should().Be(3000);
        }

        //[Fact]
        public async Task ShouldUpdateProductFromShoppingCart()
        {
            //arrange
            //await this.SetUpShoppingCart();
            var handler = new UpdateShoppingCartCommandHandler(_context, _mapper, _productsHttpRequestHandler.Object);
            var query = new UpdateShoppingCartCommand()
            {
                ShoppingCartIntegrationId = new Guid("0EF1268E-CCCC-DDDD-EEEE-8EB94494D896"),
                Products = new List<ShoppingCartProductExternal> {
                        new ShoppingCartProductExternal()
                        {
                            Id = 7,
                            Quantity = 0,
                        }
                    }
            };
            //_productsHttpRequestHandler.Setup(x => x.GetProductsByIntegrationIds(It.IsAny<List<Guid>>()))
            //    .ReturnsAsync(new GetProductsByIntegrationIdQueryResult
            //    {
            //        Products = new List<ProductExternal>()
            //        {
            //            new ProductExternal()
            //                {
            //                    Id = 5
            //                }
            //        }
            //    });
            //act
            var result = await handler.Handle(
                  query,
                  cancellationToken: CancellationToken.None);
            //assert
            result.Should().BeOfType<UpdateShoppingCartCommandResult>();
            result.ErrorDescription.Should().BeNull();
            result.PositiveMessage.Should().Be($"Successfully updated shopping cart identify by integrationId 0ef1268e-cccc-dddd-eeee-8eb94494d896");


            var handlerGet = new GetShoppingCartByIdQueryHandler(_context, _mapper);
            var queryGet = new GetShoppingCartByIdQuery()
            {
                ExternalContract = new GetShoppingCartByIdQueryExternal
                {
                    ShoppingCartId = 2
                }
            };
            var resultGet = await handlerGet.Handle(
                  queryGet,
                  cancellationToken: CancellationToken.None);
            resultGet.ShoppingCartDto.Should().BeOfType<ShoppingCartDto>();
            resultGet.ShoppingCartDto.ShoppingCartProducts.Should().BeEmpty();
        }

        private async Task SetUpShoppingCart1()
        {
            var shoppingCart = new ShopingCarts.Domain.Entities.ShoppingCart
            {
                Id = 2,
                IntegrationId = new Guid("0EF1268E-CCCC-DDDD-EEEE-8EB94494D896"),
                UserId = 1,
                Total = 3000
            };
            var product = new ShoppingCartProduct
            {
                Id = 5,
                ProductIntegrationId = new Guid("0EF1268E-33D6-49CD-A4B5-8EB94494D896"),
                Quantity = 1,
                Price = 3000,
                ShoppingCartId = 2,
            };

            await this._context.ShoppingCarts.AddAsync(shoppingCart);
            await this._context.ShoppingCartProducts.AddAsync(product);
            await this._context.SaveChangesAsync(CancellationToken.None);
        }

        private async Task SetUpShoppingCart2()
        {
            var shoppingCart2 = new ShopingCarts.Domain.Entities.ShoppingCart
            {
                Id = 3,
                IntegrationId = new Guid("0EF1268E-FFFF-FFFF-FFFF-8EB94494D896"),
                UserId = 1,
                Total = 3000
            };
            var product2 = new ShoppingCartProduct
            {
                Id = 6,
                ProductIntegrationId = new Guid("0EF1268E-33D6-49CD-A4B5-8EB94494D896"),
                Quantity = 1,
                Price = 3000,
                ShoppingCartId = 3,
            };
            var product3 = new ShoppingCartProduct
            {
                Id = 7,
                ProductIntegrationId = new Guid("23363AFF-DD71-4F3C-8381-F7E71021761E"),
                Quantity = 1,
                Price = 4000,
                ShoppingCartId = 3,
            };

            await this._context.ShoppingCarts.AddAsync(shoppingCart2);
            await this._context.ShoppingCartProducts.AddAsync(product2);
            await this._context.ShoppingCartProducts.AddAsync(product3);
            await this._context.SaveChangesAsync(CancellationToken.None);
        }

        private async Task SetUpShoppingCart3()
        {
            var shoppingCart = new ShopingCarts.Domain.Entities.ShoppingCart
            {
                Id = 4,
                IntegrationId = new Guid("0EF1268E-CCCC-CCCC-CCCC-8EB94494D896"),
                UserId = 1,
                Total = 3000
            };
            var product = new ShoppingCartProduct
            {
                Id = 8,
                ProductIntegrationId = new Guid("0EF1268E-33D6-49CD-A4B5-8EB94494D896"),
                Quantity = 1,
                Price = 3000,
                ShoppingCartId = 4,
            };
            var product2 = new ShoppingCartProduct
            {
                Id = 9,
                ProductIntegrationId = new Guid("23363AFF-DD71-4F3C-8381-F7E71021761E"),
                Quantity = 1,
                Price = 4000,
                ShoppingCartId = 4,
            };

            await this._context.ShoppingCarts.AddAsync(shoppingCart);
            await this._context.ShoppingCartProducts.AddAsync(product);
            await this._context.ShoppingCartProducts.AddAsync(product2);
            await this._context.SaveChangesAsync(CancellationToken.None);
        }
    }
}
