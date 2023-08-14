using AutoMapper;
using Moq;
using ShopingCarts.Application.Mapping;
using ShopingCarts.ExternalServices.SynchComunication.HttpClients.Abstract;
using ShopingCarts.Persistance.Context;

namespace ShoppingCart.Tests.Common
{
    public class CommandTestBase : IDisposable
    {
        internal ShoppingCartContext Context { get; private set; }
        public IMapper Mapper { get; private set; }
        internal Mock<IProductHttpService> ProductHttpService { get; private set; }
        internal Mock<IOrderHttpService> OrderHttpService { get; private set; }

        public CommandTestBase()
        {
            Context = ShoppingCartContextFactory.Create().Object;

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();

            ProductHttpService = new Mock<IProductHttpService>();

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

        [CollectionDefinition("CheckoutCommandCollection")]
        public class CheckoutCommandTestBase : ICollectionFixture<CommandTestBase>
        {
        }
    }
}