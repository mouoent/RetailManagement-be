using Microsoft.EntityFrameworkCore;
using RetailManagement_be.Models.Entities;
using System.Text.Json.Serialization;
using System.Text.Json;
using RetailManagement_be.Persistence.Interfaces;

namespace RetailManagement_be.Persistence.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly RetailManagementDbContext _context;
    protected BaseRepository(RetailManagementDbContext context) => _context = context;

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        if (entity is null) return null;
        DetachEntity(entity);

        return entity;
    }

    public async Task<List<TEntity>> GetAllAsync()
        => await _context.Set<TEntity>().AsNoTracking().ToListAsync();

    public async Task AddAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);        
    }

    public async Task AddRangeAsync(List<TEntity> entities)
    {
        await _context.Set<TEntity>().AddRangeAsync(entities);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }

    public async Task DeleteAsync(int entityId)
    {
        var entityToDelete = await GetByIdAsync(entityId);

        if (entityToDelete != null)
        {
            _context.Set<TEntity>().Remove(entityToDelete);
        }
    }

    // Prevents navigation properties from creating cyclic loops
    private void DetachEntity(TEntity entity)
    {
        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles, // Handle cycles
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        _ = JsonSerializer.Serialize(entity, options); // Force serialization to resolve loops
    }
}
