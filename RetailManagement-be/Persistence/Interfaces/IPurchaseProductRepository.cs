using RetailManagement_be.Models.Entities;

namespace RetailManagement_be.Persistence.Interfaces;

public interface IPurchaseProductRepository : IBaseRepository<PurchaseProduct>
{
    Task DeleteByPurchaseIdAsync(int purchaseId);
    Task<bool> IsProductLinkedToPurchases(int productId);
}
