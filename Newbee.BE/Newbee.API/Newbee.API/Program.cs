
using Bosta.API;
using Bosta.API.Manager;
using Bosta.API.Services.ApiCall;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newbee.API.Email;
using Newbee.BLL.Services.Auth;
using Newbee.BLL.Services.Email;
using Newbee.BLL.Setting;
using Newbee.DAL.Repository;
using Newbee.Entities.Interfaces;

namespace Newbee.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region Bosta Area
            builder.Services.Configure<BostaAppSettings>(builder.Configuration.GetSection("Bosta"));
            builder.Services.AddHttpClient<IApiCall, ApiCall>();
            builder.Services.AddScoped<IBostaManager, BostaManager>();
            #endregion



            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDependencies(builder.Configuration);
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAuthServices, AuthServices>();
            builder.Services.AddScoped<EmailBuilder>();
            //builder.Services.AddScoped<IEmailSender, EmailSender>();
        
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

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
