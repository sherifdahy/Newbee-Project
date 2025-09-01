using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newbee.BLL.Services.Email;
using Newbee.DAL.Data;
using Newbee.Entities;

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