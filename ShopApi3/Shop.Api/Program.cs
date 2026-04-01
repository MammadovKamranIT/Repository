using Microsoft.EntityFrameworkCore;
using Shop.Api.Extensions;
using Shop.Application.Extensions;

using Shop.Infrastructure.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddIdentityAndJwt(builder.Configuration);


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOrAbove", policy => policy.RequireClaim("Permission", "UserOrAbove"));
});


builder.Services.AddSwagger();

builder.Services.AddJwtAuthenticationAndAuthorization(builder.Configuration);




var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services.AddDbContext<Shop.Data.ShopDbContext>(options =>
    options.UseSqlServer(connectionString)); 


var app = builder.Build();

app.UseOrderPipeLine();
await app.EnsureRolesSeededAsync();


app.Run();