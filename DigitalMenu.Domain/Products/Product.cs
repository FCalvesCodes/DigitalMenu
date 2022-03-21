using DigitalMenu.Domain.Core.Entities;

namespace DigitalMenu.Domain.Products
{
    public class Product : Entity<Guid>
    {
        public virtual string Name { get; set; }
        public virtual string? Description { get; set; }
        public virtual decimal Price { get; set; }
    }
}
