namespace ShopingCarts.ExternalServices.SynchComunication.HttpClients.Abstract
{
    public interface IProductHttpService
    {
        Task<List<Integrations.Products.Results.ProductDto>> GetProductsByIntegratinoIds (List<Guid> integrationIds);
    }
}
