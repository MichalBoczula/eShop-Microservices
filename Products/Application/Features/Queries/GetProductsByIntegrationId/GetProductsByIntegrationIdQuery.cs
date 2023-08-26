using Integrations.Products.GetProductsByIntegrationId.Requests;
using Integrations.Products.GetProductsByIntegrationId.Results;
using MediatR;

namespace Products.Application.Features.Queries.GetProductsByIntegrationId
{
    internal class GetProductsByIntegrationIdQuery : IRequest<GetProductsByIntegrationIdQueryResult>
    {
        public GetProductsByIntegrationIdQueryExternal ExternalContract { get; set; }
    }
}
