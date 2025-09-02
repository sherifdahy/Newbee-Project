namespace Newbee.API.AppConfiguration;

public class MappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        // this use when update
        config.NewConfig<Platform, Platform>()
            .Ignore(x=>x.Id).Ignore(x=>x.CreatedAt);

        // this use when update
        config.NewConfig<Company, Company>()
            .Ignore(x => x.Id).Ignore(x => x.CreatedAt);

        // this use when update
        config.NewConfig<Customer, Customer>()
            .Ignore(x => x.Id).Ignore(x => x.CreatedAt);

        // this use when update
        config.NewConfig<Product, Product>()
            .Ignore(x => x.Id).Ignore(x => x.CreatedAt);

        // this use when update
        config.NewConfig<ProductCategory,ProductCategory>()
            .Ignore(x => x.Id).Ignore(x => x.CreatedAt);

    }
}
