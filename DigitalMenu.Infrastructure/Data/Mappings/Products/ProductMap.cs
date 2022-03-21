using DigitalMenu.Domain.Products;
using FluentNHibernate.Mapping;

namespace DigitalMenu.Infrastructure.Data.Mappings.Products
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Id(c => c.Id).GeneratedBy.GuidComb();

            Map(c => c.Name).Length(150).Not.Nullable();
            Map(c => c.Description).Length(600);
            Map(c => c.Price).Default("0.0").Not.Nullable();
        }
    }
}
