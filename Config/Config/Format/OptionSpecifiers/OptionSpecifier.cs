using Config.Options;

namespace Config.Format.OptionSpecifiers
{
	public abstract class OptionSpecifier
	{
		public string Name { get; set; }

		public bool Required { get; set; }

		public object DefaultValue { get; set; }

		protected OptionSpecifier(string name, bool required = false, object defaultValue = null)
		{
			Name = name;
			Required = required;
			DefaultValue = defaultValue;
		}

		internal abstract Option Parse(string value);

		internal Option Parse(object value)
		{
			return Parse(value.ToString());
		}
	}

	public abstract class OptionSpecifier<T> : OptionSpecifier
	{
		protected OptionSpecifier(string name, bool required, T defaultValue) 
			: base(name, required, defaultValue)
		{
		}
	}
}