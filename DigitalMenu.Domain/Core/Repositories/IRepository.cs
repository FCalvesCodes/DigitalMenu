using DigitalMenu.Domain.Core.Entities;

namespace DigitalMenu.Domain.Core.Repositories
{
    public interface IRepository
    {
    }

    public interface IRepository<TEntity, TId> : IRepository
        where TId : struct
        where TEntity : IEntity<TId>
    {
        Task<TEntity> GetAsync(TId id);
        Task<TEntity> GetFullAsync(TId id);
        Task<IList<TEntity>> GetAllAsync();
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TId id);
    }
}
