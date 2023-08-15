using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orders.Application.Contracts;
using Orders.Application.Features.Common;
using Orders.Application.Features.Queries.GetOrdersByUserIntegrationId.Dtos;
using System.Linq;

namespace Orders.Application.Features.Queries.GetOrdersByUserIntegrationId
{
    internal class GetOrdersByUserIntegrationIdQueryHandler : QueryBase, IRequestHandler<GetOrdersByUserIntegrationIdQuery, GetOrdersByUserIntegrationIdQueryResult>
    {
        public GetOrdersByUserIntegrationIdQueryHandler(IOrderContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<GetOrdersByUserIntegrationIdQueryResult> Handle(GetOrdersByUserIntegrationIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await this._context.Orders
                    .Include(x => x.UserRef)
                    .Where(x => x.UserRef.IntegrationId == request.ExternalContract.UserIntegrationId)
                    .Include(x => x.OrderProducts)
                    .ProjectTo<OrderDto>(this._mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                if (result.Any())
                {
                    return new GetOrdersByUserIntegrationIdQueryResult
                    {
                        Orders = result,
                        ErrorDescription = null
                    };
                }
                else
                {
                    return new GetOrdersByUserIntegrationIdQueryResult
                    {
                        Orders = null,
                        ErrorDescription = $"Orders for user identify by {request.ExternalContract.UserIntegrationId} doesn't exist"
                    };
                }
            }
            catch (Exception ex)
            {
                return new GetOrdersByUserIntegrationIdQueryResult
                {
                    Orders = null,
                    ErrorDescription = $"Orders  can not be retrive, because occured error wit message {ex.Message}"
                };
            }


        }
    }
}
