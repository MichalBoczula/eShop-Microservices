namespace ShopingCarts.Domain.Entities
{
    internal class ShoppingCart
    {
        public int Id { get; set; }
        public List<ShoppingCartProduct> ShoppingCartProducts { get; set; }
        public int UserId { get; set; }
        public User UserRef { get; set; }
        public int Total { get; set; }
        public Guid IntegrationId { get; set; }
    }
}
