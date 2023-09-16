namespace Orders.Domain.Entities
{
    internal class OrderProduct
    {
        public int Id { get; set; }
        public Guid ProductIntegrationId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int OrderId { get; set; }
        public Order OrderRef { get; set; }
    }
}
