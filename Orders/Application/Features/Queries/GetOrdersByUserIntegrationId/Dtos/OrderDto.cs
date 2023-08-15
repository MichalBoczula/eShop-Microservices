using AutoMapper;
using Orders.Application.Mapping;
using Orders.Domain.Entities;

namespace Orders.Application.Features.Queries.GetOrdersByUserIntegrationId.Dtos
{
    public class OrderDto : IMapFrom<Order>
    {
        public List<OrderProductDto> OrderProducts { get; set; }
        public int Total { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderDto>();
        }
    }
}
