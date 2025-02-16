using Microsoft.EntityFrameworkCore;
using RetailManagement_be.Models.Entities;
using RetailManagement_be.Persistence;
using RetailManagement_be.Persistence.Interfaces;

namespace RetailManagement_be.Persistence.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(RetailManagementDbContext context) : base(context)
    {
    }

    public async Task<List<Product>> GetByIdsAsync(List<int> ids)
    {
        var products = await _context.Products.Where(p => ids.Contains(p.Id)).AsNoTracking().ToListAsync();

        if (products is null || products.Count == 0) return new List<Product>();

        return products;
    }
}
