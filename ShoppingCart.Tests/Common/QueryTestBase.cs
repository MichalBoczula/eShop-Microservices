using AutoMapper;
using ShopingCarts.Application.Mapping;
using ShopingCarts.Persistance.Context;

namespace ShoppingCart.Tests.Common
{
    public class QueryTestBase : IDisposable
    {
        internal ShoppingCartContext Context { get; private set; }
        public IMapper Mapper { get; private set; }

        public QueryTestBase()
        {
            Context = ShoppingCartContextFactory.Create().Object;

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
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
