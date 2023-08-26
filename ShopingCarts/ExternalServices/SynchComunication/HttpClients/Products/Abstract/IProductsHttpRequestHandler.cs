using Integrations.Products.GetProductsByIntegrationId.Results;

namespace ShopingCarts.ExternalServices.SynchComunication.HttpClients.Products.Abstract
{
    internal interface IProductsHttpRequestHandler
    {
        Task<GetProductsByIntegrationIdQueryResult> GetProductsByIntegrationIds(List<Guid> integrationIds);
    }
}
