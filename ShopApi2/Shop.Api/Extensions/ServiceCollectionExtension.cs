
using Shop.Data;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Reflection;
using System.Text;
using Shop.Application.Config;
using Shop.Infrastructure.Identity;

namespace Shop.Api.Extensions
{
    public static class ServiceCollectionExtension
    {


        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddOpenApi();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Shop API",
                    Description = "This API includes full CRUD operations for the Shop project.",
                    Contact = new OpenApiContact { Name = "Shop Team", Email = "support@Shop.com" },
                    License = new OpenApiLicense { Name = "MIT License", Url = new Uri("https://opensource.org/license/mit") }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                    options.IncludeXmlComments(xmlPath);

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: Authorization: Bearer {token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
            
            });

            return services;
        }


        public static IServiceCollection AddJwtAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfig = new JwtConfig();
            configuration.GetSection(JwtConfig.SectionName).Bind(jwtConfig);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidAudience = jwtConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey!)),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization(options =>
            {

                options.AddPolicy("UserOrAbove", policy => policy.RequireRole("Admin", "Manager", "User"));
           
            });



            return services;
        }

        public static IServiceCollection AddIdentityAndJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
            })
            .AddEntityFrameworkStores<ShopDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }




    }
}
