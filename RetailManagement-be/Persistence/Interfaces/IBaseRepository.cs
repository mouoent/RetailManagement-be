using RetailManagement_be.Models.Entities;

namespace RetailManagement_be.Persistence.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(List<TEntity> entities);
        Task DeleteAsync(int entityId);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
        Task UpdateAsync(TEntity entity);
    }
}