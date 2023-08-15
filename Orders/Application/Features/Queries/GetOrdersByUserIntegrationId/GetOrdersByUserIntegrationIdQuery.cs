using MediatR;

namespace Orders.Application.Features.Queries.GetOrdersByUserIntegrationId
{
    internal class GetOrdersByUserIntegrationIdQuery : IRequest<GetOrdersByUserIntegrationIdQueryResult>
    {
        public GetOrdersByUserIntegrationIdQueryExternal ExternalContract { get; set; }
    }
}
