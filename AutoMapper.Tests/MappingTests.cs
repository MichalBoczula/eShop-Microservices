namespace AutoMapper.Tests
{
    public class MappingTests
    {
        [Fact]
        public void ProductsAutomapperTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Products.Application.Mapping.MappingProfile>();
            });

            config.AssertConfigurationIsValid();
        }

        [Fact]
        public void ShoppingCartsAutomapperTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ShopingCarts.Application.Mapping.MappingProfile>();
            });

            config.AssertConfigurationIsValid();
        }
    }
}