using BookStoreAPI._Core.ServiceFactories;

namespace BookStoreAPI.Infrastructure.Injects
{
    public static class InjectScopedServicesExtenions
    {
        public static IServiceCollection InjectScopedServices(this IServiceCollection services)
        {
            return services;
        }
    }

    public static class InjectSingletonServicesExtenions
    {
        public static IServiceCollection InjectSingletonServices(this IServiceCollection services)
        {            
            services.AddSingleton<IDatabaseDapper, DatabaseDapperExtension>();

            return services;
        }
    }
}
