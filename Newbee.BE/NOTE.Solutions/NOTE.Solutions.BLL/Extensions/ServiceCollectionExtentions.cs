using Microsoft.Extensions.DependencyInjection;

namespace NOTE.Solutions.BLL.Extensions;

public static class ServiceCollectionExtentions
{
    public static void test(this ServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(IServiceCollection).Assembly);
    }
}
