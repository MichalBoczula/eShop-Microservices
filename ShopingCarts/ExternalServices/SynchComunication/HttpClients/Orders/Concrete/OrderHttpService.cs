using Integrations.Orders.Request;
using Integrations.Orders.Results;
using ShopingCarts.ExternalServices.SynchComunication.HttpClients.Orders.Abstract;

namespace ShopingCarts.ExternalServices.SynchComunication.HttpClients.Orders.Concrete
{
    internal class OrderHttpService : IOrderHttpService
    {
        public Task<CreateOrderDto> CreateOrder(ShoppingCartExternal shoppingCartExternal)
        {
            throw new NotImplementedException();
        }
    }
}
