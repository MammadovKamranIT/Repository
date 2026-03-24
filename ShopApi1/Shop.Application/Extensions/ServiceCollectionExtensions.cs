using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Services;
using Shop.Application.Services.Interfaces;
using Shop.Mappings;
using Shop.Services;
using Shop.Services.Interfaces;


namespace Shop.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {



        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
