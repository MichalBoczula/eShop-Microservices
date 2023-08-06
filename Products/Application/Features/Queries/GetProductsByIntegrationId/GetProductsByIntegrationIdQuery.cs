using MediatR;

namespace Products.Application.Features.Queries.GetProductsByIntegrationId
{
    internal class GetProductsByIntegrationIdQuery : IRequest<GetProductsByIntegrationIdQueryResult>
    {
        public GetProductsByIntegrationIdQueryExternal ExternalContract { get; set; }
    }
}
