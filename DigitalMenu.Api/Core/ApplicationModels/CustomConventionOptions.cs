namespace DigitalMenu.Api.Core.ApplicationModels
{
    public class CustomConventionOptions
    {
        public IList<string> IgnorePluralPrefixes { get; set; } = new List<string>();
        public bool UsePlural { get; set; } = false;
        public CustomConventionNomenclatureType NomenclatureType { get; set; } = CustomConventionNomenclatureType.KebabCase;
        public CustomConventionTextType TextType { get; set; } = CustomConventionTextType.LowerCase;
    }
}
