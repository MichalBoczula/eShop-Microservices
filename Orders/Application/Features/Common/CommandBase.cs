using AutoMapper;
using Orders.Application.Contracts;

namespace Orders.Application.Features.Common
{
    internal class CommandBase
    {
        protected readonly IOrderContext _context;
        protected readonly IMapper _mapper;

        public CommandBase(IOrderContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
