using NHibernate;

namespace DigitalMenu.Infrastructure.Core.Data.Nhibernate.Contracts
{
    public interface INhDataContext : IDataContext
    {
        ISession Session { get; }
        Task MergeAsync<T>(T entity);
    }
}
