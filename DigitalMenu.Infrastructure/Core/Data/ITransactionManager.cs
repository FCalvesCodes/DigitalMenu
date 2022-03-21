namespace DigitalMenu.Infrastructure.Core.Data
{
    public interface ITransactionManager : IDisposable
    {
        bool TransactionActive { get; }

        void BeginTransaction();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
