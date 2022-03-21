using DigitalMenu.Application.Core.Applications;
using DigitalMenu.Application.Model.Products;

namespace DigitalMenu.Application.Contracts
{
    public interface IProductAppService : ICrudAppService<Guid, ProductDto>
    {
    }
}
