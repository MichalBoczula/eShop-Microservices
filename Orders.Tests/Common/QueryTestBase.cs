using AutoMapper;
using Orders.Application.Mapping;
using Orders.Persistance.Context;

namespace Orders.Tests.Common
{
    public class QueryTestBase : IDisposable
    {
        internal OrderContext Context { get; private set; }
        public IMapper Mapper { get; private set; }

        public QueryTestBase()
        {
            Context = OrdersContextFactory.Create().Object;

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            OrdersContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestBase>
    {
    }
}