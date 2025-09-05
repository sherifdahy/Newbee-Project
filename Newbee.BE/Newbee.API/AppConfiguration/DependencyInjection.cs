using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newbee.BLL.Authentication;
using Newbee.BLL.Services;
using System.Reflection;
using System.Text;

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
            .AddMapsterConfig()
            .AddControllers();

        var connectionString = configuration.GetConnectionString("default") ??
           throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });


        return services;
    }
    private static IServiceCollection AddMapsterConfig(this IServiceCollection services)
    {
        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton<IMapper>(new Mapper(mappingConfig));
        return services;
    }
    private static IServiceCollection AddBusinessLogicConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthServices, AuthServices>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IPlatformService,PlatformService>();
        services.AddScoped<ICompanyService,CompanyService>();
        services.AddScoped<IProductCategoryService,ProductCategoryService>();
        services.AddScoped<IZoneServices, ZoneServices>();
        services.AddScoped<ICountryServices, CountryServices>();
        services.AddScoped<ICityServices, CityServices>();
        services.AddScoped<EmailBuilder>();
        return services;
    }
    private static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Newbee API",
                Version = "v1",
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
        });
        return services;
    }
    private static IServiceCollection AddBostaConfig(this IServiceCollection services,IConfiguration configuration)
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

        services.AddSingleton<IJwtProvider, JwtProvider>();

        services.AddOptions<JwtOptions>()
            .BindConfiguration("Jwt")
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var jwtSettings = configuration.GetSection("Jwt").Get<JwtOptions>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key!)),
                ValidIssuer = jwtSettings?.Issuer,
                ValidAudience = jwtSettings?.Audience
            };
        });

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 8;
            options.User.RequireUniqueEmail = true;
        });


        return services;
    }
}