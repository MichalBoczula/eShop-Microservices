using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Orders.Application.Contracts;
using Orders.Application.Features.Common;
using Orders.Domain.Entities;

namespace Orders.Application.Features.Commands.CreateOrder
{
    internal class CreateOrderCommandHandler : CommandBase, IRequestHandler<CreateOrderCommand, CreateOrderCommandResult>
    {
        public CreateOrderCommandHandler(IOrderContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<CreateOrderCommandResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await this._context.Users.FirstOrDefaultAsync(x => x.IntegrationId == request.ExternalContract.ShoppingCart.UserIntegrationId);

                if (user == null)
                {
                    return new CreateOrderCommandResult
                    {
                        PositiveMessage = null,
                        ErrorDescription = $"User identify by {request.ExternalContract.ShoppingCart.UserIntegrationId} doesn't exist",
                    };
                }
                else
                {

                    var order = this._mapper.Map<Order>((request.ExternalContract.ShoppingCart, user));
                        
                    var products = this._mapper.Map<List<OrderProduct>>(request.ExternalContract.ShoppingCart.Products);

                    order.OrderProducts = products;

                    this._context.Orders.Add(order);
                    await this._context.SaveChangesAsync(cancellationToken);

                    return new CreateOrderCommandResult
                    {
                        PositiveMessage = $"Order identify by integration Id {request.ExternalContract.ShoppingCart.ShoppingCartIntegrationId} has been created",
                        ErrorDescription = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new CreateOrderCommandResult
                {
                    PositiveMessage = null,
                    ErrorDescription = $"During creating order proccess for user identify by {request.ExternalContract.ShoppingCart.UserIntegrationId} occured error with message {ex.Message}",
                };
            }

        }
    }
}
