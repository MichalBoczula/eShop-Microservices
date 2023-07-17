namespace Products.Application.Features.Queries.GetProductsByIntegrationId
{
    internal class GetProductsByIntegrationIdQueryResult
    {
        public List<ProductDto>? Products { get; set; }
        public string? ErrorDescription { get; set; }
    }
}
