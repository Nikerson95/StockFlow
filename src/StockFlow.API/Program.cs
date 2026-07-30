using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using StockFlow.Infrastructure.Persistence;
using StockFlow.Application.Repositories;
using StockFlow.Infrastructure.Repositories;
using StockFlow.Application.Categories.Create;
using StockFlow.Application.Categories.GetAll;
using StockFlow.Application.Categories.GetById;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString(
    "DefaultConnection");

builder.Services.AddDbContext<StockFlowDbContext>(options =>
    options.UseSqlServer(connectionString));

    builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
    builder.Services.AddScoped<CreateCategoryUseCase>();
    builder.Services.AddScoped<GetAllCategoriesUseCase>();
    builder.Services.AddScoped<GetCategoryByIdUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}


// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
