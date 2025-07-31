using AdvertisingPlatform.API.Extensions;

namespace AdvertisingPlatform.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var services = builder.Services;
        var logging = builder.Logging;

        logging.ConfigureLogger();

        // Application
        services.AddAdvertiserParsers();
        services.AddMediatRHandlers();

        // Infrastructure
        services.AddDbContexts();
        services.AddRepositories();

        services.AddAuthorization();

        // API
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
