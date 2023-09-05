namespace ShopingCarts.Application.Features.Commands.AddProductToShoppingCart
{
    public class AddProductToShoppingCartCommandExternal
    {
        public int? ShoppingCartProductId { get; set; }
        public Guid? ShoppingCartProductIntegrationId { get; set; }
        public int ShoppingCartProductQuantity { get; set; }
    }
}
