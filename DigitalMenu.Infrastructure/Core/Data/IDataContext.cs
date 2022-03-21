namespace DigitalMenu.Infrastructure.Core.Data
{
    public interface IDataContext : IDisposable
    {
        IQueryable<T> Query<T>();
        Task<T> GetAsync<T, TId>(TId id);
        Task CreateAsync<T>(T entity);
        Task UpdateAsync<T>(T entity);
        Task DeleteAsync<T>(T entity);
        Task SaveChangesAsync();
    }
}
