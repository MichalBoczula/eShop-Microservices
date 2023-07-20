namespace ShopingCarts.Domain.Entities
{
    internal class User
    {
        public int Id { get; set; }
        public Guid IntegrationId { get; set; }
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCartRef { get; set; }
    }
}
