namespace Products.Domain.Entities
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string ImgName { get; set; }
        public Guid IntegrationId { get; set; }
    }
}
