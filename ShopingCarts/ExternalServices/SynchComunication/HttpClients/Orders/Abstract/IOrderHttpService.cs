using Integrations.Orders.Request;
using Integrations.Orders.Results;

namespace ShopingCarts.ExternalServices.SynchComunication.HttpClients.Orders.Abstract
{
    internal interface IOrderHttpService
    {
        Task<CreateOrderDto> CreateOrder(ShoppingCartExternal shoppingCartExternal);
    }
}
