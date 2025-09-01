using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newbee.BLL.Authentication;
using Newbee.BLL.Services.Email;
using Newbee.DAL.Data;
using Newbee.Entities;
using System.Text;

namespace Newbee.API;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthConfig(configuration);
        var connectionString = configuration.GetConnectionString("default") ??
           throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

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

        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
        var setting = configuration.GetSection("Jwt").Get<JwtOptions>();
        services.AddSingleton<IJwtProvider, JwtProvider>();
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(o =>
        {
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting?.Key!)),
                ValidIssuer =setting?.Issuer,
                ValidAudience = setting?.Audience

            };
        });
        return services;
    }
    //public static void ConfigureServices(IServiceCollection services)
    //{
    //    services.AddScoped<EmailBuilder>(sp =>
    //    {
    //        var env = sp.GetRequiredService<IWebHostEnvironment>();
    //        return new EmailBuilder(env.WebRootPath);
    //    });
    //}


}