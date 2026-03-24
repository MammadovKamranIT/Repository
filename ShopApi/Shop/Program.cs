using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shop.Api.Extensions;
using Shop.Application.Extensions;
using Shop.Data;
using Shop.Infrastructure.Extensions;
using Shop.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);


builder.Services.AddSwagger();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<Shop.Data.ShopDbContext>(options =>
    options.UseSqlServer(connectionString)); 




var app = builder.Build();

app.UseOrderPipeLine();


app.Run();