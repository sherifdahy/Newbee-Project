using Bosta.API.BostaSettings;
using Bosta.API.Manager;
using Bosta.API.Services.ApiCall;
using Microsoft.AspNetCore.Mvc;

namespace Newbee.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Bosta
            builder.Services.Configure<BostaAppSettings>(builder.Configuration.GetSection("Bosta"));
            builder.Services.AddHttpClient<IApiCall, ApiCall>();
            builder.Services.AddScoped<IBostaManager, BostaManager>();
            #endregion

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            builder.Services.AddControllers().AddJsonOptions(o=>
            {
                o.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });
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
