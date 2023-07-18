namespace ShopingCarts.Domain.Entities
{
    internal class ShoppingCartProduct
    {
        public int Id { get; set; }
        public Guid ProductIntegrationId { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCartRef { get; set; }
    }
}
