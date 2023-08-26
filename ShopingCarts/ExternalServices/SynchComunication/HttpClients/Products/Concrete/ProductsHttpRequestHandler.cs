using Integrations.Common;
using Integrations.Products.GetProductsByIntegrationId.Requests;
using Integrations.Products.GetProductsByIntegrationId.Results;
using ShopingCarts.ExternalServices.SynchComunication.HttpClients.Products.Abstract;
using System.Net.Http;

namespace ShopingCarts.ExternalServices.SynchComunication.HttpClients.Concrete.Products
{
    internal class ProductsHttpRequestHandler : IProductsHttpRequestHandler
    {
        private readonly IProductHttpService _productHttpService;
        private readonly IConfiguration _configuration;

        public ProductsHttpRequestHandler(IProductHttpService productHttpService, IConfiguration configuration)
        {
            _productHttpService = productHttpService;
            _configuration = configuration;
        }

        public async Task<GetProductsByIntegrationIdQueryResult> GetProductsByIntegrationIds(List<Guid> integrationIds)
        {
            var url = this._configuration["ProductsUrl:GetProductsByIntegrationId"];
            var contract = new GetProductsByIntegrationIdQueryExternal { IntegrationIds = integrationIds };

            var response = await this._productHttpService.PostAsJsonAsync(url, contract);

            var result = await Utilities.GetResponseContent<GetProductsByIntegrationIdQueryResult>(response);

            return result;
        }
    }
}
