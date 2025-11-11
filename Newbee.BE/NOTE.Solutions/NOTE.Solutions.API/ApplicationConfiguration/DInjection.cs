using ETA.Consume;
using ETA.Consume.Interfaces;
using ETA.Consume.Manager;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NOTE.Solutions.BLL.Authentication;
using NOTE.Solutions.BLL.Authentication.Filters;
using NOTE.Solutions.BLL.Services;
using NOTE.Solutions.DAL.Data;
using NOTE.Solutions.DAL.Repository;
using NOTE.Solutions.Entities.Entities.Identity;
using NOTE.Solutions.Entities.Interfaces;
using System.Reflection;
using System.Text;

namespace NOTE.Solutions.API.ApplicationConfiguration;

public static class DInjection
{
    public static IServiceCollection DI(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddSwaggerConfig();
        services.AddDbConfig(configuration);
        services.AddServicesConfig();
        services.AddAuthConfig(configuration);
        services.AddCorsConfig(configuration);
        services.AddFluentValidationConfig();
        services.AddEtaConfig(configuration);
        services.AddMapsterConfig();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        services.AddHangFireConfig(configuration);
        services.AddHealthChecksConfig(configuration);
        services.AddIdentityDbContextConfig(configuration);

        return services;
    }
    private static IServiceCollection AddHangFireConfig(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddHangfireServer();
        services.AddHangfire(config =>
        {
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180);
            config.UseSimpleAssemblyNameTypeSerializer();
            config.UseRecommendedSerializerSettings();
            config.UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection"));
        });
        return services;
    }

    private static IServiceCollection AddMapsterConfig(this IServiceCollection services)
    {
        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        var bllAssembly = Assembly.Load("NOTE.Solutions.BLL");

        mappingConfig.Scan(bllAssembly);
        services.AddSingleton<IMapper>(new Mapper(mappingConfig));

        return services;
    }

    private static IServiceCollection AddHealthChecksConfig
        (this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("default")!;
        if(connectionString is null) throw new Exception("Invalid Connection String");

        services
            .AddHealthChecks()
            .AddSqlServer(name: "database", connectionString: connectionString)
            .AddUrlGroup(name:"external api", uri : new Uri("https://id.preprod.eta.gov.eg"))
            .AddHangfire(option =>
            {
                option.MinimumAvailableServers = 1;
                option.MaximumJobsFailed = 5;
            });
        
        return services;
    }

    private static IServiceCollection AddCorsConfig(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.WithOrigins(configuration.GetSection("AllowedOrigins").Get<string[]>()!);
            });
        });
        return services;
    }
    private static IServiceCollection AddFluentValidationConfig(this IServiceCollection services)
    {
        var validationAssembly = Assembly.Load("NOTE.Solutions.BLL");
        services.AddFluentValidationAutoValidation().AddValidatorsFromAssembly(validationAssembly);
        return services;
    }
    private static IServiceCollection AddAuthConfig(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddSingleton<IJWTProvider, JWTProvider>();

        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

        var jwtSettings = configuration.GetSection("Jwt").Get<JwtOptions>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.SaveToken = true;
            // validation 
            o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateActor = true,
                ValidateLifetime = true,

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings!.Key)),
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
            };
        }).AddCookie(IdentityConstants.ApplicationScheme, options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            options.SlidingExpiration = false;
        });

        services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();


        return services;
    }
    private static IServiceCollection AddServicesConfig(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICompanyService, CompanyService>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<ICityService, CityService>();
        services.AddScoped<IGovernateService, GovernateService>();
        services.AddScoped<IActiveCodesService, ActiveCodeService>();
        services.AddScoped<ICompanyActiveCodesService, CompanyActiveCodesService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IManagerService, ManagerService>();
        services.AddScoped<IUnitService, UnitService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductUnitService, ProductUnitService>();
        services.AddScoped<IEtaManager, EtaManager>();
        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IPointOfSaleService,PointOfSaleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IBranchService, BranchService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<BLL.Interfaces.IReceiptService, BLL.Services.ReceiptService>();


        services.AddDistributedMemoryCache();

        return services;
    }
    private static IServiceCollection AddDbConfig(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration?.GetConnectionString("default");

        if (connectionString is null)
            throw new Exception("Invalid Connection String");

        services.AddDbContext<ApplicationDbContext>(x =>
        {
            x.UseSqlServer(connectionString).EnableSensitiveDataLogging();
        });
        
        return services;
    }
    private static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "NOTE.Solutions API",
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
    private static IServiceCollection AddEtaConfig(this IServiceCollection services,IConfiguration configuration)
    {
        services.Configure<ETAOptions>(configuration.GetSection("ETA"));
        return services;
    }
    private static IServiceCollection AddIdentityDbContextConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentityCore<ApplicationUser>(options =>
        {
            // password options
            options.Password.RequiredLength = 6;
            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = false;

            // requird confirmation
            //options.SignIn.RequireConfirmedEmail = true;

            // lockout options
            options.Lockout.AllowedForNewUsers = true;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;

        })
        .AddRoles<ApplicationRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddSignInManager<SignInManager<ApplicationUser>>()
        .AddDefaultTokenProviders();
        return services;
    }
}
