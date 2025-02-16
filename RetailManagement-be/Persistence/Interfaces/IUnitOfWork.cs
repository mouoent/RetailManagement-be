namespace RetailManagement_be.Persistence.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    ICustomerRepository Customers { get; }
    IPurchaseRepository Purchases { get; }
    IPurchaseProductRepository PurchaseProducts { get; }

    Task<int> CompleteAsync();
}
