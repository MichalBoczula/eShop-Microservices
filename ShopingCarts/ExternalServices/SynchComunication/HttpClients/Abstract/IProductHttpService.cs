namespace ShopingCarts.ExternalServices.SynchComunication.HttpClients.Abstract
{
    internal interface IProductHttpService
    {
        Task<List<Integrations.Products.Results.ProductDto>> GetProductsByIntegratinoIds (List<Guid> integrationIds);
    }
}
