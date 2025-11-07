using ProductManagement.Repositories;
using ProductManagement.Repositories.Interface;
using ProductManagement.Services;
using ProductManagement.Services.Interface;

namespace ProductManagement.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductService, ProductService>();

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICategoryService, CategoryService>();

            return services;
        }
    }
}
