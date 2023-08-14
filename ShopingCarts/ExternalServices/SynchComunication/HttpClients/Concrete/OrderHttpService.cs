using Integrations.Orders.Results;
using ShopingCarts.ExternalServices.SynchComunication.HttpClients.Abstract;

namespace ShopingCarts.ExternalServices.SynchComunication.HttpClients.Concrete
{
    internal class OrderHttpService : IOrderHttpService
    {
        public Task<CreateOrderDto> CreateOrder(Guid ShoppingCartIntegrationId)
        {
            throw new NotImplementedException();
        }
    }
}
