using AutoMapper;
using MediatR;
using Orders.Application.Contracts;
using Orders.Application.Features.Common;

namespace Orders.Application.Features.Commands.CreateOrder
{
    internal class CreateOrderCommandHandler : CommandBase, IRequestHandler<CreateOrderCommand, CreateOrderCommandResult>
    {
        public CreateOrderCommandHandler(IOrderContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<CreateOrderCommandResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
