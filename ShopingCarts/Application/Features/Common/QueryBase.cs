using AutoMapper;
using ShopingCarts.Application.Contracts;

namespace ShopingCarts.Application.Features.Common
{
    internal abstract class QueryBase
    {
        protected readonly IShoppingCartContext _context;
        protected readonly IMapper _mapper;

        public QueryBase(IShoppingCartContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
