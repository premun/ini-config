namespace Config.Format
{
	internal class OptionSpecifier
	{
		internal string Name { get; set; }

		internal object DefaultValue { get; set; }

		internal bool Required { get; set; }

		internal OptionSpecifier(string name, bool required = false, object defaultValue = null)
        {
            Name = name;
			Required = required;
			DefaultValue = defaultValue;
		}
	}
}