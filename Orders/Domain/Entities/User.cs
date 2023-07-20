namespace Orders.Domain.Entities
{
    internal class User
    {
        public int Id { get; set; }
        public Guid IntegrationId { get; set; }
        public List<Order> Orders { get; set; }
    }
}
