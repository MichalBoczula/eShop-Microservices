using Integrations.Products.GetProductsByIntegrationId.Requests;
using Integrations.Products.GetProductsByIntegrationId.Results;

namespace ShopingCarts.ExternalServices.SynchComunication.HttpClients.Products.Abstract
{
    internal interface IProductHttpService
    {
        Task<GetProductsByIntegrationIdQueryResult> GetProductsByIntegrationId(string url, GetProductsByIntegrationIdQueryExternal contract);
    }
}
