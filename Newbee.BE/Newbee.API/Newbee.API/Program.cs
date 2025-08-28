
using Bosta.API;
using Bosta.API.Manager;
using Bosta.API.Services.ApiCall;
using Microsoft.Extensions.Configuration;

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


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
