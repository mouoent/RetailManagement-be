using Microsoft.EntityFrameworkCore;
using RetailManagement_be.Models.Entities;
using RetailManagement_be.Persistence;
using RetailManagement_be.Persistence.Interfaces;

namespace RetailManagement_be.Persistence.Repositories;

public class PurchaseRepository : BaseRepository<Purchase>, IPurchaseRepository
{
    public PurchaseRepository(RetailManagementDbContext context) : base(context)
    {
    }

    public async Task<bool> HasPurchasesForCustomer(int customerId)
    {
        return await _context.Purchases.AnyAsync(p => p.CustomerId == customerId);
    }
}
