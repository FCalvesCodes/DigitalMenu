using DigitalMenu.Api.Core.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Pluralize.NET.Core;

namespace DigitalMenu.Api.Core.ApplicationModels
{
    public abstract class CustomConventionBase
    {
        protected readonly Pluralizer pluralizer = new Pluralizer();

        protected virtual void RemoveEmptySelectors(IList<SelectorModel> selectors)
        {
            selectors
                .Where(c => c.AttributeRouteModel == null && c.ActionConstraints.IsNullOrEmpty())
                .ToList()
                .ForEach(s => selectors.Remove(s));
        }

        protected virtual string NormalizeNomenclatureType(string name, CustomConventionNomenclatureType? type)
        {
            if (name.IsNullOrEmpty())
            {
                return name;
            }

            if (type == CustomConventionNomenclatureType.SnakeCase)
            {
                return name.ToSnakeCase();
            }

            if (type == CustomConventionNomenclatureType.KebabCase)
            {
                return name.ToKebabCase();
            }

            return name;
        }

        protected virtual string NormalizeTextType(string name, CustomConventionTextType? type)
        {
            if (name.IsNullOrEmpty())
            {
                return name;
            }

            if (type == CustomConventionTextType.UpperCase)
            {
                return name.ToUpper();
            }

            if (type == CustomConventionTextType.LowerCase)
            {
                return name.ToLower();
            }

            return name;
        }

        protected string Pluralize(string name)
        {
            if (name.IsNullOrEmpty())
            {
                return name;
            }

            return pluralizer.Pluralize(name);
        }
    }
}
