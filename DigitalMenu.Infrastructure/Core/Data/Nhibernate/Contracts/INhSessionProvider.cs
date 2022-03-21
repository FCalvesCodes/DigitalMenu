using NHibernate;

namespace DigitalMenu.Infrastructure.Core.Data.Nhibernate.Contracts
{
    public interface INhSessionProvider : IDisposable
    {
        ISession Session { get; }
        IStatelessSession StatelessSession { get; }
    }
}
