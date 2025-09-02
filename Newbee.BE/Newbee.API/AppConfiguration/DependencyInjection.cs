using Newbee.BLL.Authentication;
using Newbee.BLL.Services;

namespace Newbee.API.AppConfiguration;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddBusinessLogicConfig(configuration)
            .AddSwaggerConfig()
            .AddBostaConfig(configuration)
            .AddAuthConfig(configuration)
            .AddControllers();

        var connectionString = configuration.GetConnectionString("default") ??
           throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        return services;
    }

    public static IServiceCollection AddBusinessLogicConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthServices, AuthServices>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IProductCategoryService,ProductCategoryService>();
        services.AddScoped<EmailBuilder>();
        return services;
    }
    public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
    public static IServiceCollection AddBostaConfig(this IServiceCollection services,IConfiguration configuration)
    {
        //services.Configure<BostaAppSettings>(configuration.GetSection("Bosta"));
        //services.AddHttpClient<IApiCall, ApiCall>();
        //services.AddScoped<IBostaManager, BostaManager>();

        return services;
    }
    private static IServiceCollection AddAuthConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<ApplicationUser, IdentityRole<int>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 8;
            options.User.RequireUniqueEmail = true;
        });

        return services;
    }
}