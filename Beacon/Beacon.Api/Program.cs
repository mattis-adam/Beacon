using Beacon.Api.Sources;
using Microsoft.OpenApi;

namespace Beacon.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSwagger", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            builder.Services.AddHttpClient<StatuspageClient>(c =>
            {
                c.Timeout = TimeSpan.FromSeconds(10);
                c.DefaultRequestHeaders.UserAgent.ParseAdd("Beacon/1.0");
            });
            builder.Services.AddControllers();
            builder.Services.AddSingleton<IReadOnlyList<MonitoredService>>(MonitoredServices.All);

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Beacon Api Swagger",
                    Version = "v1"
                });
            });

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Kairo v1");
                c.RoutePrefix = "swagger";
            });
            app.UseCors("AllowSwagger");

            app.UseAuthorization();
            app.MapControllers();
            app.MapGet("/health", () => Results.Ok(new { ok = true, dotnet = Environment.Version.ToString() }));
            app.Run();
        }

    }
}