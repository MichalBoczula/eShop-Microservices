namespace ShopingCarts.Domain.Entities
{
    internal class User
    {
        public int Id { get; set; }
        public Guid IntegrationId { get; set; }
        public List<ShoppingCart> ShoppingCarts { get; set; }
    }
}
