using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace DigitalMenu.Api.Core.ApplicationModels
{
    public class CustomControllerConvention : CustomConventionBase, IControllerModelConvention
    {
        protected readonly CustomControllerConventionIOptions options = new CustomControllerConventionIOptions();

        public CustomControllerConvention(Action<CustomControllerConventionIOptions> action)
        {
            action?.Invoke(options);
        }

        public void Apply(ControllerModel controller)
        {
            NormalizeName(controller);
        }

        private void NormalizeName(ControllerModel controller)
        {
            if (!controller.RouteValues.ContainsKey("controller"))
            {
                controller.ControllerName = Pluralize(controller.ControllerName);
                controller.ControllerName = NormalizeTextType(controller.ControllerName, options.TextType);
                controller.ControllerName = NormalizeNomenclatureType(controller.ControllerName, options.NomenclatureType);
            }
        }
    }
}
