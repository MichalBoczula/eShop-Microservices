using AutoMapper;
using Products.Application.Mapping;
using Products.Domain.Entities;

namespace Products.Application.Features.Queries.GetProductsByIntegrationId
{
    public class ProductDto : IMapFrom<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string ImgName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDto>();
        }
    }
}
