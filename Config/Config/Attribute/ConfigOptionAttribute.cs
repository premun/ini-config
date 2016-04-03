namespace Config.Attribute
{
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class ConfigOptionAttribute : System.Attribute
    {
        private readonly string _section;
        private readonly string _option;

        public ConfigOptionAttribute(string section, string option)
        {
            _section = section;
            _option = option;
        }
    }
}