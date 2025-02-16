using RetailManagement_be.Shared.Exceptions;
using RetailManagement_be.Interfaces.Services;
using RetailManagement_be.Models.DTOs.Purchase;
using RetailManagement_be.Models.Entities;
using RetailManagement_be.Persistence.Interfaces;

namespace RetailManagement_be.Services;

public class PurchaseService : IPurchaseService
{
    private readonly IUnitOfWork _unitOfWork;

    public PurchaseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PurchaseDto> AddPurchase(CreatePurchaseDto purchaseDto)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(purchaseDto.CustomerId)
            ?? throw new NotFoundException<Customer>();

        // Count occurrences of each product ID to determine quantity
        var productQuantityMap = purchaseDto.ProductIds
            .GroupBy(id => id)
            .ToDictionary(group => group.Key, group => group.Count()); // ProductId -> Quantity

        // Fetch unique product IDs
        var uniqueProductIds = purchaseDto.ProductIds.Distinct().ToList();
        var products = await _unitOfWork.Products.GetByIdsAsync(uniqueProductIds);

        if (products.Count != uniqueProductIds.Count)
            throw new ValidationException("Some products not found");

        // Create Purchase entity
        var purchase = new Purchase
        {
            CustomerId = customer.Id
        };

        await _unitOfWork.Purchases.AddAsync(purchase);
        await _unitOfWork.CompleteAsync();

        // Link products to the purchase using PurchaseProduct
        var purchaseProducts = productQuantityMap.Select(kv => new PurchaseProduct
        {
            PurchaseId = purchase.Id,
            ProductId = kv.Key,
            Quantity = kv.Value 
        }).ToList();

        await _unitOfWork.PurchaseProducts.AddRangeAsync(purchaseProducts);
        await _unitOfWork.CompleteAsync();

        return new PurchaseDto
        {
            Id = purchase.Id,
            CustomerId = customer.Id,
            ProductIds = purchaseProducts.Select(pp => pp.ProductId).ToList()
        };
    }

    public async Task DeletePurchase(int id)
    {
        var purchase = await _unitOfWork.Purchases.GetByIdAsync(id) ?? throw new NotFoundException<Purchase>();

        // Remove related PurchaseProduct records first
        await _unitOfWork.PurchaseProducts.DeleteByPurchaseIdAsync(id);

        await _unitOfWork.Purchases.DeleteAsync(id);

        await _unitOfWork.CompleteAsync();
    }
}
