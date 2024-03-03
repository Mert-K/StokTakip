using Microsoft.Extensions.DependencyInjection;
using Service.Abstract;
using Service.BusinessRules;
using Service.Concrete;

namespace Service;
//IOC kayıtları için extension method oluşturma
public static class ServiceDependencies
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ProductRules>();
        services.AddScoped<CategoryRules>();

        return services;
    }
}
