
using Shop.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Shop.Application.Interfaces;
using Shop.Infrastructure.Repositories;

namespace Shop.Infrastructure.Extensions
{
    public static class InfrastructureServiceCollectionExtensions
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
     

            var connectionString = configuration.GetConnectionString("DefaultConnectionString");
            services.AddDbContext<ShopDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();



            return services;
        }


    }
}
