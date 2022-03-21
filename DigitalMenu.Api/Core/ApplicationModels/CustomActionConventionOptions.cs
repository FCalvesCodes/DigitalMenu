namespace DigitalMenu.Api.Core.ApplicationModels
{
    public class CustomActionConventionOptions : CustomConventionOptions
    {
        public IDictionary<string, string> RemovePrefixes { get; set; } = new Dictionary<string, string>();

        public CustomActionConventionOptions SetPrefixe(string actionName, CustomConventionHttpVerbs verb)
        {
            RemovePrefixes.Add(actionName, verb.ToString());
            return this;
        }
    }
}
