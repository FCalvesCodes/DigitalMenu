namespace DigitalMenu.Application.Core.WorkUnits
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangesAsync();
        Task CompleteAsync();
    }
}
