using MediatR;
using Products.Application.Features.Queries.GetAllProducts;

namespace Products.Application.Features.Queries.GetProductsByIntegrationId
{
    internal class GetProductsByIntegrationIdQuery : IRequest<GetProductsByIntegrationIdQueryResult>
    {
        public GetProductsByIntegrationIdQueryExternal ExternalContract { get; set; }
    }
}
