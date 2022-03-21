using DigitalMenu.Api.Core.Controllers;
using DigitalMenu.Application.Contracts;
using DigitalMenu.Application.Model.Products;

namespace DigitalMenu.Api.Controllers
{
    public class ProductController : CrudController<Guid, ProductDto, IProductAppService>
    {
        public ProductController(IProductAppService appService) : base(appService)
        {
        }
    }
}
