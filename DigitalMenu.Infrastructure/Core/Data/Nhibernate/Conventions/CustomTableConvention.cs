using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using Pluralize.NET.Core;

namespace DigitalMenu.Infrastructure.Core.Data.Nhibernate.Conventions
{
    public class CustomTableConvention : IClassConvention
    {
        protected readonly Pluralizer pluralizer = new Pluralizer();

        public void Apply(IClassInstance instance)
        {
            if (instance.TableName.Equals(instance.EntityType.Name))
            {
                return;
            }

            var tableName = pluralizer.Pluralize(instance.EntityType.Name).ToLower();
            instance.Table(tableName);
        }
    }
}
