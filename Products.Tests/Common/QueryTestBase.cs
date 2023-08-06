using AutoMapper;
using Products.Application.Mapping;
using Products.Persistance.Context;

namespace Products.Tests.Common
{
    public class QueryTestBase : IDisposable
    {
        internal ProductsContext Context { get; private set; }
        public IMapper Mapper { get; private set; }

        public QueryTestBase()
        {
            Context = ProductsManagerDbContextFactory.Create().Object;

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            ProductsManagerDbContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestBase>
    {
    }
}
