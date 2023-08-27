using Integrations.Common;
using Integrations.Products.GetProductsByIntegrationId.Requests;
using Integrations.Products.GetProductsByIntegrationId.Results;
using ShopingCarts.ExternalServices.SynchComunication.HttpClients.Products.Abstract;

namespace ShopingCarts.ExternalServices.SynchComunication.HttpClients.Concrete.Products
{
    internal class ProductHttpService : IProductHttpService
    {
        private readonly HttpClient _httpClient;

        public ProductHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetProductsByIntegrationIdQueryResult> GetProductsByIntegrationId(string url, GetProductsByIntegrationIdQueryExternal contract)
        {
            var response = await _httpClient.PostAsJsonAsync(url, contract);
            var result = await Utilities.GetResponseContent<GetProductsByIntegrationIdQueryResult>(response);
            return result;
        }
    }
}