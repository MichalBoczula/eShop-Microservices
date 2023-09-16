using AutoMapper;
using Moq;
using ShopingCarts.Application.Mapping;
using ShopingCarts.ExternalServices.SynchComunication.HttpClients.Orders.Abstract;
using ShopingCarts.ExternalServices.SynchComunication.HttpClients.Products.Abstract;
using ShopingCarts.Persistance.Context;

namespace ShoppingCart.Tests.Common
{
    public class CommandTestBase : IDisposable
    {
        internal ShoppingCartContext Context { get; private set; }
        internal IMapper Mapper { get; private set; }
        internal Mock<IProductsHttpRequestHandler> ProductsHttpRequestHandler { get; private set; }
        internal Mock<IOrderHttpService> OrderHttpService { get; private set; }

        public CommandTestBase()
        {
            Context = ShoppingCartContextFactory.Create().Object;

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();

            ProductsHttpRequestHandler = new Mock<IProductsHttpRequestHandler>();

            OrderHttpService = new Mock<IOrderHttpService>();
        }

        public void Dispose()
        {
            ShoppingCartContextFactory.Destroy(Context);
        }

        [CollectionDefinition("AddCommandCollection")]
        public class AddCommandTestBase : ICollectionFixture<CommandTestBase>
        {
        }

        [CollectionDefinition("RemoveCommandCollection")]
        public class RemoveCommandTestBase : ICollectionFixture<CommandTestBase>
        {
        }

        [CollectionDefinition("UpdateCommandCollection")]
        public class CheckoutCommandTestBase : ICollectionFixture<CommandTestBase>
        {
        }
    }
}