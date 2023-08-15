using Orders.Application.Features.Queries.GetOrdersByUserIntegrationId.Dtos;

namespace Orders.Application.Features.Queries.GetOrdersByUserIntegrationId
{
    public class GetOrdersByUserIntegrationIdQueryResult
    {
        public List<OrderDto>? Orders { get; set; }
        public string? ErrorDescription { get; set; }
    }
}
