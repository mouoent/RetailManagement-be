using Microsoft.AspNetCore.Mvc;
using RetailManagement_be.Interfaces.Services;
using RetailManagement_be.Models.DTOs.Product;

namespace RetailManagement_be.Endpoints;

public static class ProductsEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async ([FromServices] IProductService service) => await service.GetProducts());
        app.MapGet("/{id:int}", async ([FromServices] IProductService service, int id) =>
        {
            var product = await service.GetProductById(id);
            return product is not null ? Results.Ok(product) : Results.NotFound();
        });
        app.MapPost("/", async (IProductService service, [FromBody] CreateProductDto product) =>
        {
            int id = await service.AddProduct(product);
            return Results.Created($"/{id}", product);
        });
        app.MapPut("/", async ([FromServices] IProductService service, [FromBody] UpdateProductDto product) =>
        {
            await service.UpdateProduct(product);
            return Results.Ok("Product updated.");
        });
        app.MapDelete("/{id:int}", async ([FromServices] IProductService service, int id) =>
        { 
            await service.DeleteProduct(id);
            return Results.NoContent();
        });
    }
}
