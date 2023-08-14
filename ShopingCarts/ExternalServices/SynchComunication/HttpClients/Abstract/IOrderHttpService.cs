using Integrations.Orders.Results;

namespace ShopingCarts.ExternalServices.SynchComunication.HttpClients.Abstract
{
    internal interface IOrderHttpService
    {
        Task<CreateOrderDto> CreateOrder(Guid ShoppingCartIntegrationId);
    }
}
