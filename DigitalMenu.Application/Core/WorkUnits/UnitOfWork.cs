using DigitalMenu.Infrastructure.Core.Data;

namespace DigitalMenu.Application.Core.WorkUnits
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDataContext dataContext;
        private readonly ITransactionManager transactionManager;

        public UnitOfWork(IDataContext dataContext, ITransactionManager transactionManager)
        {
            this.dataContext = dataContext;
            this.transactionManager = transactionManager;
        }

        public async Task CompleteAsync()
        {
            await SaveChangesAsync();

            if (transactionManager.TransactionActive)
            {
                await transactionManager.CommitAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await dataContext.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}
