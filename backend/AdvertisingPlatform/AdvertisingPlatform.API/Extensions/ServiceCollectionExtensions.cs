using AdvertisingPlatform.Application.Repositories.Interfaces;
using AdvertisingPlatform.Application.Utils;
using AdvertisingPlatform.Infrastructure.Contexts.InMemory;
using AdvertisingPlatform.Infrastructure.Repositories;
using AdvertisingPlatform.Infrastructure.Utils;
using System.Reflection;

namespace AdvertisingPlatform.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services)
        {
            services.AddSingleton<InMemoryContext>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IAdvertiserRepository, InMemoryAdvertiserRepository>();

            return services;
        }

        public static IServiceCollection AddMediatRHandlers(this IServiceCollection services)
        {
            services.AddMediatR(o =>
            {
                var assembly = Assembly.Load("AdvertisingPlatform.Application");

                o.RegisterServicesFromAssembly(assembly);
            });

            return services;
        }

        public static IServiceCollection AddAdvertiserParsers(this IServiceCollection services)
        {
            services.AddScoped<IAdvertiserParser, AdvertiserParserStackPool>();

            return services;
        }
    }
}
