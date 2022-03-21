using DigitalMenu.Domain.Products;
using DigitalMenu.Infrastructure.Core.Data;
using DigitalMenu.Infrastructure.Core.Data.Repositories;

namespace DigitalMenu.Infrastructure.Data.Repositories
{
    public class ProductRepository : Repository<Product, Guid>, IProductRepository
    {
        public ProductRepository(IDataContext context) : base(context)
        {
        }
    }
}
