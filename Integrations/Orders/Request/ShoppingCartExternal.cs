namespace Integrations.Orders.Request
{
    public class ShoppingCartExternal
    {
        public List<ShoppingCartProductExternal> Products { get; set; }
        public int Total { get; set; }
        public Guid UserIntegrationId { get; set; }
        public Guid ShoppingCartIntegrationId { get; set; }
    }
}
