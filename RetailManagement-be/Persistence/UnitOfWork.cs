using RetailManagement_be.Persistence.Interfaces;

namespace RetailManagement_be.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly RetailManagementDbContext _context;

    public IProductRepository Products { get; }
    public ICustomerRepository Customers { get; }
    public IPurchaseRepository Purchases { get; }
    public IPurchaseProductRepository PurchaseProducts { get; }

    public UnitOfWork(RetailManagementDbContext context,
                      IProductRepository productRepository,
                      ICustomerRepository customerRepository,
                      IPurchaseRepository purchaseRepository,
                      IPurchaseProductRepository purchaseProductRepository)
    {
        _context = context;
        Products = productRepository;
        Customers = customerRepository;
        Purchases = purchaseRepository;
        PurchaseProducts = purchaseProductRepository;
    }

    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
