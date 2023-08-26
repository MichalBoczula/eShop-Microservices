using AutoMapper;
using Integrations.Products.GetProductsByIntegrationId.Results;
using Products.Application.Features.Queries.GetProductsByIntegrationId;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AutoMapper.Tests")]
namespace Products.Application.Mapping
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.ApplyMappingFromAssembly(Assembly.GetExecutingAssembly());
            this.CreateProductExternalForGetProductsByIntegrationId();
        }

        private void ApplyMappingFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes().Where(
                    p => p.GetInterfaces().Any(i =>
                        i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            types.ForEach(
                t =>
                {
                    var instance = Activator.CreateInstance(t);
                    var method = t.GetMethod("Mapping");
                    method?.Invoke(instance, new object[] { this });
                });
        }

        private void CreateProductExternalForGetProductsByIntegrationId()
        {
            CreateMap<ProductDto, ProductExternal>();
        }
    }
}
