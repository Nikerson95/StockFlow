using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using StockFlow.Infrastructure.Persistence;
using StockFlow.Application.Repositories;
using StockFlow.Infrastructure.Repositories;
using StockFlow.Application.Categories.Create;
using StockFlow.Application.Categories.GetAll;
using StockFlow.Application.Categories.GetById;
using StockFlow.Application.Categories.Update;
using StockFlow.Application.Categories.Delete;
using StockFlow.Application.Products.Create;
using StockFlow.Application.Products.GetAll;
using StockFlow.Application.Products.GetById;
using StockFlow.Application.Products.Update;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString(
    "DefaultConnection");

builder.Services.AddDbContext<StockFlowDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<GetProductByIdUseCase>();

builder.Services.AddScoped<CreateCategoryUseCase>();
builder.Services.AddScoped<GetAllCategoriesUseCase>();
builder.Services.AddScoped<GetCategoryByIdUseCase>();
builder.Services.AddScoped<UpdateCategoryUseCase>();
builder.Services.AddScoped<DeleteCategoryUseCase>();
builder.Services.AddScoped<GetAllProductsUseCase>();

builder.Services.AddScoped<CreateProductUseCase>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
