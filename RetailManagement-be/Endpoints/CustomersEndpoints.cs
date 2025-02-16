using Microsoft.AspNetCore.Mvc;
using RetailManagement_be.Interfaces.Services;
using RetailManagement_be.Models.DTOs.Customer;

namespace RetailManagement_be.Endpoints;

public static class CustomersEndpoints
{
    public static void MapCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", async ([FromServices] ICustomerService service) => await service.GetCustomers());
        app.MapGet("/{id:int}", async ([FromServices] ICustomerService service, int id) =>
        {
            var product = await service.GetCustomerById(id);
            return product is not null ? Results.Ok(product) : Results.NotFound();
        });
        app.MapPost("/", async ([FromServices] ICustomerService service, [FromBody] CreateCustomerDto customerDto) =>
        {
            int id = await service.AddCustomer(customerDto);
            return Results.Created($"/{id}", customerDto);
        });
        app.MapPut("/", async([FromServices] ICustomerService service, [FromBody] UpdateCustomerDto customer) =>
        {
            await service.UpdateCustomer(customer);
            return Results.Ok("Customer updated.");
        });
        app.MapDelete("/{id:int}", async ([FromServices] ICustomerService service, int id) =>
        {
            await service.DeleteCustomer(id);
            return Results.NoContent();            
        });
    }
}
