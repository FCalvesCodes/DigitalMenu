namespace DigitalMenu.Infrastructure.Core.Data.Nhibernate
{
    public class NhibernateOptions
    {
        public NhibernateSchemaUpdateOptions SchemaUpdate { get; set; }
        public string[]? MappingAssemblies { get; set; }
        public string? Dialect { get; set; }
        public string? DefaultSchema { get; set; }
        public string? ConnectionStringName { get; set; }

        public NhibernateOptions()
        {
            SchemaUpdate = new NhibernateSchemaUpdateOptions();
        }
    }
}
