using Integrations.Products.Results;
using ShopingCarts.ExternalServices.SynchComunication.HttpClients.Abstract;

namespace ShopingCarts.ExternalServices.SynchComunication.HttpClients.Concrete
{
    internal class ProductHttpService : IProductHttpService
    {
        public Task<List<ProductDto>> GetProductsByIntegratinoIds(List<Guid> integrationIds)
        {
            throw new NotImplementedException();
        }
    }
}
