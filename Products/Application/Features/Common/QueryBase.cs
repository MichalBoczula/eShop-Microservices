using AutoMapper;
using Products.Application.Contracts.Persistance;

namespace Products.Application.Features.Common
{
    internal abstract class QueryBase
    {
        protected readonly IProductsContext _context;
        protected readonly IMapper _mapper;

        public QueryBase(IProductsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
