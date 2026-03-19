using Shop.Mappings;
using Shop.Services;
using Shop.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;


namespace Shop.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {



        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}
