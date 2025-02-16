using Microsoft.EntityFrameworkCore;
using RetailManagement_be.Endpoints;
using RetailManagement_be.Interfaces.Services;
using RetailManagement_be.Middlewares;
using RetailManagement_be.Persistence;
using RetailManagement_be.Persistence.Interfaces;
using RetailManagement_be.Persistence.Repositories;
using RetailManagement_be.Services;

var builder = WebApplication.CreateBuilder(args);

var environment = builder.Environment.EnvironmentName ?? "Production";

// Set configuration
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Load configuration
var configuration = builder.Configuration;

// Add DB context
builder.Services.AddDbContext<RetailManagementDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

// Add UnitOfWork service to centralize repositories
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add repository services
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<IPurchaseProductRepository, PurchaseProductRepository>();

// Add business layer services
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Add Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Add Exception handling middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

// Minimal API Endpoints
app.MapGroup("/products").MapProductEndpoints();
app.MapGroup("/customers").MapCustomerEndpoints();
app.MapGroup("/purchases").MapPurchaseEndpoints();


app.Run();
