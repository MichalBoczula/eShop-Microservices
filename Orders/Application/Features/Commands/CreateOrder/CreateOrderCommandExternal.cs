using Integrations.Orders.Request;

namespace Orders.Application.Features.Commands.CreateOrder
{
    public class CreateOrderCommandExternal
    {
        public ShoppingCartExternal ShoppingCart { get; set; }
    }
}
