namespace DigitalMenu.Infrastructure.Core.Data.Nhibernate
{
    public class NhibernateSchemaUpdateOptions
    {
        public bool DoUpdate { get; set; }
        public bool SaveToFile { get; set; }

        internal bool AllowsInvokeSchemaUpdate()
        {
            return DoUpdate || SaveToFile;
        }
    }
}
