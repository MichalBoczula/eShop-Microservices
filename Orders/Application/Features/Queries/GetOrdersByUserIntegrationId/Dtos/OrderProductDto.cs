using AutoMapper;
using Orders.Application.Mapping;
using Orders.Domain.Entities;

namespace Orders.Application.Features.Queries.GetOrdersByUserIntegrationId.Dtos
{
    public class OrderProductDto : IMapFrom<OrderProduct>
    {
        public Guid ProductIntegrationId { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderProduct, OrderProductDto>();
        }
    }
}
