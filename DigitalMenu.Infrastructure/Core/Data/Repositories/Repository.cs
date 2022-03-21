using DigitalMenu.Domain.Core.Entities;
using DigitalMenu.Domain.Core.Repositories;
using NHibernate.Linq;

namespace DigitalMenu.Infrastructure.Core.Data.Repositories
{
    public abstract class Repository : IRepository
    {
        protected readonly IDataContext context;

        protected Repository(IDataContext context)
        {
            this.context = context;
        }

    }

    public abstract class Repository<TEntity, TId> : Repository, IRepository<TEntity, TId>
        where TId : struct
        where TEntity : class, IEntity<TId>
    {
        public Repository(IDataContext context) : base(context)
        { }

        public virtual async Task<TEntity> GetAsync(TId id)
        {
            return await context.GetAsync<TEntity, TId>(id);
        }

        public virtual async Task<TEntity> GetFullAsync(TId id)
        {
            return await GetAsync(id);
        }

        public virtual async Task<IList<TEntity>> GetAllAsync()
        {
            return await context.Query<TEntity>().ToListAsync();
        }

        public async virtual Task DeleteAsync(TEntity entity)
        {
            await context.DeleteAsync(entity);
        }

        public async virtual Task DeleteAsync(TId id)
        {
            await DeleteAsync(await GetAsync(id));
        }

        public virtual async Task CreateAsync(TEntity entity)
        {
            await context.CreateAsync(entity);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            await context.UpdateAsync(entity);
        }
    }
}
