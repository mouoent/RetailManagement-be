using Microsoft.EntityFrameworkCore;
using RetailManagement_be.Models.Entities;
using RetailManagement_be.Persistence;
using RetailManagement_be.Persistence.Interfaces;

namespace RetailManagement_be.Persistence.Repositories;

public class PurchaseProductRepository : BaseRepository<PurchaseProduct>, IPurchaseProductRepository
{
    public PurchaseProductRepository(RetailManagementDbContext context) : base(context)
    {
    }

    public async Task DeleteByPurchaseIdAsync(int purchaseId)
    {
        var purchaseProducts = await _context.PurchaseProducts.Where(pp => pp.PurchaseId == purchaseId).ToListAsync();

        _context.PurchaseProducts.RemoveRange(purchaseProducts);
    }

    public async Task<bool> IsProductLinkedToPurchases(int productId)
    {
        return await _context.PurchaseProducts.AnyAsync(p => p.ProductId == productId);
    }
}
