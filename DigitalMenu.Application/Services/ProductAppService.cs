using AutoMapper;
using DigitalMenu.Application.Contracts;
using DigitalMenu.Application.Core.Applications;
using DigitalMenu.Application.Model.Products;
using DigitalMenu.Domain.Products;

namespace DigitalMenu.Application.Services
{
    public class ProductAppService : CrudAppService<Product, Guid, ProductDto, IProductRepository>, IProductAppService
    {
        public ProductAppService(IMapper mapper, IProductRepository repository) : base(mapper, repository)
        {
        }
    }
}
