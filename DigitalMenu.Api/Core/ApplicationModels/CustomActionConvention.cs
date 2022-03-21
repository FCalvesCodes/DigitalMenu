using DigitalMenu.Api.Core.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace DigitalMenu.Api.Core.ApplicationModels
{
    public class CustomActionConvention : CustomConventionBase, IActionModelConvention
    {
        private readonly CustomActionConventionOptions options = new CustomActionConventionOptions();

        public CustomActionConvention(Action<CustomActionConventionOptions> action)
        {
            action?.Invoke(options);
        }

        public void Apply(ActionModel action)
        {
            RemoveEmptySelectors(action.Selectors);

            var verb = options.RemovePrefixes
                    .FirstOrDefault(c => action.ActionName.StartsWithIgnoreCase(c.Key));

            if (!action.Selectors.Any())
            {
                var selectorModel = new SelectorModel
                {
                    AttributeRouteModel = CreateAttributeRoute(action, verb.Value),
                };

                selectorModel.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { verb.Value }));
                action.Selectors.Add(selectorModel);
            }
            else
            {
                foreach (var selector in action.Selectors.Where(c => c.AttributeRouteModel == null))
                {

                    selector.AttributeRouteModel = CreateAttributeRoute(action, verb.Value);
                }
            }
        }

        protected AttributeRouteModel CreateAttributeRoute(ActionModel action, string verb)
        {
            var conventionActionName = GetConventionActionName(action, verb);
            var conventionRouteTemplate = GetConventionRouteTemplate(action, conventionActionName);

            return new AttributeRouteModel(new RouteAttribute(conventionRouteTemplate));
        }

        protected virtual string GetConventionRouteTemplate(ActionModel action, string routeActionName)
        {
            return string.Empty
                .ConcatString($"/{action.Controller.ControllerName}")
                .ConcatStringIf(action.Parameters.Any(c => c.Name == "id"), "/{id}")
                .ConcatStringIf(!routeActionName.IsNullOrEmpty(), $"/{routeActionName}");
        }

        protected virtual string GetConventionActionName(ActionModel action, string verb)
        {
            var actionName = action.ActionName;
            var prefixFounded = false;

            if (!action.Attributes.Any(c => c is ActionNameAttribute actionNameAttribute && !actionNameAttribute.Name.IsNullOrEmpty()))
            {
                foreach (var prefix in options.RemovePrefixes)
                {
                    if (action.ActionName.StartsWithIgnoreCase(prefix.Key))
                    {
                        actionName = action.ActionName.RemoveBeginning(prefix.Key);
                        prefixFounded = true;
                        break;
                    }
                }

                if (!prefixFounded)
                {
                    actionName = action.ActionName;
                }

                if (!actionName.IsNullOrEmpty())
                {
                    actionName = actionName.RemoveFromEndIgnoreCase("Async");
                }

                actionName = NormalizeTextType(actionName, options.TextType);
                actionName = NormalizeNomenclatureType(actionName, options.NomenclatureType);
            }

            return actionName;
        }
    }
}
