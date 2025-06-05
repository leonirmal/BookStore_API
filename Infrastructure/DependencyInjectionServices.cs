using System.Reflection;
using BookStoreAPI.Infrastructure.Injects;

namespace BookStoreAPI.Infrastructure
{
    public static class DependencyInjectionServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IWebHostEnvironment hostEnvironment)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddHttpContextAccessor();

            services = services.InjectScopedServices();
            services = services.InjectSingletonServices();

            return services;
        }
    }
}
