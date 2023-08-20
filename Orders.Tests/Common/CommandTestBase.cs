using AutoMapper;
using Orders.Application.Mapping;
using Orders.Persistance.Context;

namespace Orders.Tests.Common
{
    public class CommandTestBase : IDisposable
    {
        internal OrderContext Context { get; private set; }
        public IMapper Mapper { get; private set; }

        public CommandTestBase()
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

        [CollectionDefinition("CreateOrderCommandCollection")]
        public class AddCommandTestBase : ICollectionFixture<CommandTestBase>
        {
        }
    }
}