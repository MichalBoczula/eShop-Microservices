using MediatR;

namespace Products.Application.Features.Queries.GetAllProducts
{
    internal class GetAllProductsQuery : IRequest<GetAllProductsQueryResult>
    {
    }
}
