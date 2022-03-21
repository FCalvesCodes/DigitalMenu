using DigitalMenu.Domain.Core.Repositories;

namespace DigitalMenu.Domain.Products
{
    public interface IProductRepository : IRepository<Product, Guid>
    {
    }
}
