using AutoMapper;
using Moq;
using ShopingCarts.Application.Mapping;
using ShopingCarts.ExternalServices.SynchComunication.HttpClients.Products.Abstract;
using ShopingCarts.Persistance.Context;

namespace ShoppingCart.Tests.Common
{
    public class QueryTestBase : IDisposable
    {
        internal ShoppingCartContext Context { get; private set; }
        internal IMapper Mapper { get; private set; }
        internal Mock<IProductHttpService> ProductHttpService { get; private set; }

        public QueryTestBase()
        {
            Context = ShoppingCartContextFactory.Create().Object;

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();

            ProductHttpService = new Mock<IProductHttpService>();
        }

        public void Dispose()
        {
            ShoppingCartContextFactory.Destroy(Context);
        }

        [CollectionDefinition("QueryCollection")]
        public class QueryCollection : ICollectionFixture<QueryTestBase>
        {
        }
    }
}
