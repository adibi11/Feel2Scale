using Feel2Scale.Configuration;
using Feel2Scale.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using OpenAIApi;// Ensure you have the Npgsql.EntityFrameworkCore.PostgreSQL package installed

namespace Feel2Scale.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var Config = Feel2Scale.Configuration.Config.GetConfiguration();
            var builder = WebApplication.CreateBuilder(args);
            // Use predefined configuratin instance
                
                //Add services to the container.
                // Ensure the connection string is correctly configured in appsettings.json
               builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(Config.GetConnectionString("PostgresDB")));

            var openAiSettings = Config.GetSection("OpenAI").Get<OpenAISettings>();
            if (openAiSettings != null)
                {
                // Ensure the OpenAI settings are correctly configured in appsettings.json
                if (string.IsNullOrEmpty(openAiSettings.ApiKey) || string.IsNullOrEmpty(openAiSettings.Model))
                {
                    throw new ArgumentException("OpenAI API key or model is not configured. Please set the 'OpenAI' key in your configuration file.");
                }
            }
            else
            {
                throw new ArgumentException("OpenAI settings are not configured. Please set the 'OpenAI' key in your configuration file.");
            }
            builder.Services.AddSingleton<OpenAIService>(new OpenAIService(openAiSettings));
            builder.Services.AddControllers();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy
                        .AllowAnyOrigin() // אפשר להחליף לדומיין ספציפי
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            var app = builder.Build();
            //Add CORS policy to allow all origins, headers, and methods
            //Should be later reconfigured to allow specific origins for security reasons
           

            app.UseCors("AllowAll");
            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
