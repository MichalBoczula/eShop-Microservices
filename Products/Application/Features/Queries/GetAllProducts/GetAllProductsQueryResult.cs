using Products.Domain.Entities;

namespace Products.Application.Features.Queries.GetAllProducts
{
    internal class GetAllProductsQueryResult
    {
        public List<ProductDto>? Products  { get; set; }
        public string? ErrorDescription { get; set; }
    }
}
