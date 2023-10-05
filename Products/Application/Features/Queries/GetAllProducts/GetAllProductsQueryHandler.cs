using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Products.Application.Contracts.Persistance;
using Products.Application.Features.Common;
using Products.Domain.Entities;

namespace Products.Application.Features.Queries.GetAllProducts
{
    internal class GetAllProductsQueryHandler : QueryBase, IRequestHandler<GetAllProductsQuery, GetAllProductsQueryResult>
    {
        public GetAllProductsQueryHandler(IProductsContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<GetAllProductsQueryResult> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                //var products = await this._context.Products.ToListAsync();
                var products = new List<Product>()
                                   {
                                       new Product
                                           {
                                               Id = 1,
                                               Name = "Chinese",
                                               ImgName = "Huawei",
                                               Price = 2000,
                                               IntegrationId = new Guid("55CCEE28-E15D-4644-A7BE-2F8A93568D6F")
                                           },
                                       new Product
                                           {
                                               Id = 2,
                                               Name = "Samsung",
                                               ImgName = "Samsung",
                                               Price = 3000,
                                               IntegrationId = new Guid("0EF1268E-33D6-49CD-A4B5-8EB94494D896")
                                           },
                                       new Product
                                           {
                                               Id = 3,
                                               Name = "Iphone",
                                               ImgName = "Iphone",
                                               Price = 4000,
                                               IntegrationId = new Guid("23363AFF-DD71-4F3C-8381-F7E71021761E")
                                           }
                                   };
                var result = this._mapper.Map<List<ProductDto>>(products);
                return new GetAllProductsQueryResult { Products = result, ErrorDescription = null };
            }
            catch (Exception ex)
            {
                return new GetAllProductsQueryResult { Products = null, ErrorDescription = ex.Message };
            }
        }
    }
}
