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
using StockFlow.Application.Products.Delete;
using StockFlow.Application.Stock.Entry;
using StockFlow.Application.Stock.Exit;
using StockFlow.API.ExceptionHandlers;
using StockFlow.Application.Stock.History;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString(
    "DefaultConnection");

builder.Services.AddDbContext<StockFlowDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IStockMovementRepository, StockMovementRepository>();

builder.Services.AddScoped<CreateCategoryUseCase>();
builder.Services.AddScoped<GetAllCategoriesUseCase>();
builder.Services.AddScoped<GetCategoryByIdUseCase>();
builder.Services.AddScoped<UpdateCategoryUseCase>();
builder.Services.AddScoped<DeleteCategoryUseCase>();
builder.Services.AddScoped<CreateProductUseCase>();
builder.Services.AddScoped<GetAllProductsUseCase>();
builder.Services.AddScoped<GetProductByIdUseCase>();
builder.Services.AddScoped<UpdateProductUseCase>();
builder.Services.AddScoped<DeleteProductUseCase>();
builder.Services.AddScoped<CreateProductUseCase>();
builder.Services.AddScoped<AddStockUseCase>();
builder.Services.AddScoped<RemoveStockUseCase>();
builder.Services.AddScoped<GetStockMovementsUseCase>();


var app = builder.Build();

app.UseExceptionHandler();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
