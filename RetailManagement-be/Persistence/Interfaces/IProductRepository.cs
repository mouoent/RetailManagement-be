using RetailManagement_be.Models.Entities;

namespace RetailManagement_be.Persistence.Interfaces;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<List<Product>> GetByIdsAsync(List<int> productIds);
}
