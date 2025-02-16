using RetailManagement_be.Models.Entities;

namespace RetailManagement_be.Persistence.Interfaces;

public interface IPurchaseRepository : IBaseRepository<Purchase>
{
    Task<bool> HasPurchasesForCustomer(int customerId);
}
