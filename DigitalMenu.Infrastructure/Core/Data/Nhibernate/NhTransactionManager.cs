using DigitalMenu.Infrastructure.Core.Data.Nhibernate.Contracts;
using NHibernate;

namespace DigitalMenu.Infrastructure.Core.Data.Nhibernate
{
    public class NhTransactionManager : ITransactionManager
    {
        private readonly INhSessionProvider sessionAccessor;
        private ITransaction? transaction;

        public bool TransactionActive
        {
            get { return transaction?.IsActive ?? false; }
        }

        public NhTransactionManager(INhSessionProvider sessionAccessor)
        {
            this.sessionAccessor = sessionAccessor;
        }

        public void BeginTransaction()
        {
            if (sessionAccessor.Session.GetCurrentTransaction().IsActive)
            {
                throw new Exception("A transação já foi aberta.");
            }

            sessionAccessor.Session.Clear();
            transaction = sessionAccessor.Session.BeginTransaction();
        }

        public Task CommitAsync()
        {
            return transaction.CommitAsync();
        }

        public Task RollbackAsync()
        {
            return transaction.RollbackAsync();
        }

        public void Dispose()
        {
            transaction?.Dispose();
            sessionAccessor.Dispose();
        }
    }
}
