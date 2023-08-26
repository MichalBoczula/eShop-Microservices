namespace Integrations.Products.GetProductsByIntegrationId.Results
{
    public class GetProductsByIntegrationIdQueryResult
    {
        public List<ProductExternal>? Products { get; set; }
        public string? ErrorDescription { get; set; }
    }
}
