using Microsoft.AspNetCore.Mvc;
using RetailManagement_be.Interfaces.Services;
using RetailManagement_be.Models.DTOs.Purchase;

namespace RetailManagement_be.Endpoints;

public static class PurchasesEndpoints
{
    public static void MapPurchaseEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async ([FromServices] IPurchaseService service, [FromBody] CreatePurchaseDto purchase) =>
        {
            var purchaseDto = await service.AddPurchase(purchase);
            return Results.Created($"/{purchaseDto.Id}", purchaseDto);
        });
    }
}
