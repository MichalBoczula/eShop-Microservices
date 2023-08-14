namespace ShopingCarts.Application.Features.Commands.RemoveProductFromShoppingCart
{
    public class RemoveProductFromShoppingCartCommandExternal
    {
        public int ShoppingCartId { get; set; }
        public int ShoppingCartProductId { get; set; }
        public int ShoppingCartProductQuantity { get; set; }
    }
}
