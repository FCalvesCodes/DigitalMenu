using DigitalMenu.Infrastructure.Core.Data.Nhibernate.Contracts;
using NHibernate;

namespace DigitalMenu.Infrastructure.Core.Data.Nhibernate
{
    public class NhDataContext : INhDataContext
    {
        protected readonly INhSessionProvider sessionProvider;

        public ISession Session
        {
            get { return sessionProvider.Session; }
        }

        public NhDataContext(INhSessionProvider sessionProvider)
        {
            this.sessionProvider = sessionProvider;
        }

        public void Dispose()
        {
            sessionProvider.Dispose();
        }

        public async Task<T> GetAsync<T, TId>(TId id)
        {
            return await Session.GetAsync<T>(id);
        }

        public IQueryable<T> Query<T>()
        {
            return Session.Query<T>();
        }

        public async Task CreateAsync<T>(T entity)
        {
            await Session.SaveAsync(entity);
        }

        public async Task UpdateAsync<T>(T entity)
        {
            await Session.UpdateAsync(entity);
        }

        public async Task DeleteAsync<T>(T entity)
        {
            await Session.DeleteAsync(entity);
        }

        public async Task SaveChangesAsync()
        {
            await Session.FlushAsync();
        }

        public async Task MergeAsync<T>(T entity)
        {
            await Session.MergeAsync(entity);
        }
    }
}
