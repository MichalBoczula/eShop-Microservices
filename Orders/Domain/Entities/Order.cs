namespace Orders.Domain.Entities
{
    internal class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User UserRef { get; set; }
        public int Total { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
        public Guid IntegrationId { get; set; }
    }
}
