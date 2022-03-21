using DigitalMenu.Infrastructure.Core.Data.Nhibernate.Contracts;
using NHibernate;

namespace DigitalMenu.Infrastructure.Core.Data.Nhibernate
{
    public class NhSessionProvider : INhSessionProvider
    {
        private ISession? session;
        private IStatelessSession? statelessSession;
        private readonly ISessionFactory sessionFactory;

        public ISession Session
        {
            get
            {
                if (session == null)
                {
                    session = sessionFactory.OpenSession();
                }

                return session;
            }
        }

        public IStatelessSession StatelessSession
        {
            get { return statelessSession = sessionFactory.OpenStatelessSession(); }
        }

        public NhSessionProvider(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public void Dispose()
        {
            session?.Dispose();
            statelessSession?.Dispose();
        }
    }
}
