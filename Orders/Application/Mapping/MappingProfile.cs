using AutoMapper;
using Integrations.Orders.Request;
using Orders.Domain.Entities;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AutoMapper.Tests")]
namespace Orders.Application.Mapping
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingFromAssembly(Assembly.GetExecutingAssembly());
            this.CreateOrderForCreateOrderCommand();
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

        private void CreateOrderForCreateOrderCommand()
        {
            CreateMap<ShoppingCartProductExternal, OrderProduct>()
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.OrderId, opt => opt.Ignore())
                .ForMember(d => d.OrderRef, opt => opt.Ignore());

            CreateMap<(ShoppingCartExternal ext, User u), Order>()
                .ForMember(d => d.IntegrationId, opt => opt.MapFrom(s => s.ext.ShoppingCartIntegrationId))
                .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.u.Id))
                .ForMember(d => d.Total, opt => opt.MapFrom(s => s.ext.Total))
                .ForMember(d => d.Id, opt => opt.Ignore())
                .ForMember(d => d.UserRef, opt => opt.Ignore())
                .ForMember(d => d.OrderProducts, opt => opt.Ignore());
        }
    }
}
