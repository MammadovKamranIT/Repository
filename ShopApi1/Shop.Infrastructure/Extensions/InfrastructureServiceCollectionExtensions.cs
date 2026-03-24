
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Config;
using Shop.Application.Services.Interfaces;
using Shop.Data;
using Shop.Infrastructure.Identity;
using Shop.Infrastructure.Jwt;
using Shop.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Infrastructure.Extensions
{
    public static class InfrastructureServiceCollectionExtensions
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<JwtConfig>(configuration.GetSection(JwtConfig.SectionName));

            var connectionString = configuration.GetConnectionString("DefaultConnectionString");
            services.AddDbContext<ShopDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IAuthUserStore, AuthUserStore>();




            return services;
        }


    }
}
