using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newbee.API.AppConfiguration;
using Newbee.API.Email;
using Newbee.BLL.Setting;

namespace Newbee.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDependencies(builder.Configuration);

        //Email Service Configuration
        builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
        builder.Services.AddTransient<IEmailSender>(provider =>
        {
            var emailSettings = provider.GetRequiredService<IOptions<MailSettings>>().Value;
            return new EmailSender(
                emailSettings.Email,
                emailSettings.AppPassword,
                emailSettings.Host,
                emailSettings.SSL,
                emailSettings.Port,
                emailSettings.IsBodyHtml
            );
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            await AutoMigrate(app);

            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }

    private static async Task AutoMigrate(WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var pendingMigrations = await context.Database.GetPendingMigrationsAsync();

        if (pendingMigrations.Any())
        {
            await context.Database.MigrateAsync();
        }
    }
}