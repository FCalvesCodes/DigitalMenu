using DigitalMenu.Application.Common.Models;

namespace DigitalMenu.Application.Model.Products
{
    public class ProductDto : IEntityDto<Guid>
    {
        public Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string? Description { get; set; }
        public virtual decimal Price { get; set; }
    }
}
