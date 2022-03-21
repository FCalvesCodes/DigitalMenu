using DigitalMenu.Api.Core.Extensions;
using DigitalMenu.Api.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DigitalMenu.Api.Core.ApplicationModels
{
    public class CustomParameterConvention : IParameterModelConvention
    {
        public void Apply(ParameterModel parameter)
        {
            if (parameter.BindingInfo != null || parameter.Name == "id")
            {
                return;
            }

            parameter.BindingInfo = BindingInfo.GetBindingInfo(new[] {
                        !TypeHelper.IsPrimitiveExtendedIncludingNullable(parameter.ParameterInfo.ParameterType) && CanUseFormBodyBinding(parameter) ?
                        new FromBodyAttribute() :
                        (Attribute)new FromQueryAttribute() });
        }

        protected bool CanUseFormBodyBinding(ParameterModel parameter)
        {
            foreach (var selector in parameter.Action.Selectors)
            {
                if (selector.ActionConstraints == null)
                {
                    continue;
                }

                foreach (var actionConstraint in selector.ActionConstraints)
                {
                    if (!(actionConstraint is HttpMethodActionConstraint httpMethodActionConstraint))
                    {
                        continue;
                    }

                    if (httpMethodActionConstraint.HttpMethods.All(hm => hm.In("GET", "DELETE", "TRACE", "HEAD")))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
