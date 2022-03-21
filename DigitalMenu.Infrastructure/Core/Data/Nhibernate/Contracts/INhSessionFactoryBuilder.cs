using NHibernate;

namespace DigitalMenu.Infrastructure.Core.Data.Nhibernate.Contracts
{
    public interface INhSessionFactoryBuilder
    {
        ISessionFactory BuildSessionFactory();
    }
}
