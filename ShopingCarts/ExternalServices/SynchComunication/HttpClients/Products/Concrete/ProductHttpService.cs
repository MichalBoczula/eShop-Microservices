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

        public async Task<HttpResponseMessage> PostAsJsonAsync(string url, GetProductsByIntegrationIdQueryExternal contract)
        {
            return await _httpClient.PostAsJsonAsync(url, contract);
        }
    }
}