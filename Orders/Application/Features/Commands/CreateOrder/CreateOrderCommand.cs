using MediatR;

namespace Orders.Application.Features.Commands.CreateOrder
{
    internal class CreateOrderCommand : IRequest<CreateOrderCommandResult>
    {
        public CreateOrderCommandExternal ExternalContract { get; set; }
    }
}
