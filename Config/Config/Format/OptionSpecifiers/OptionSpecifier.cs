using Config.Options;

namespace Config.Format.OptionSpecifiers
{
	public abstract class OptionSpecifier
	{
		public string Name { get; set; }

		public bool Required { get; set; }
	}

	public abstract class OptionSpecifier<T> : OptionSpecifier
	{
		public T DefaultValue { get; set; }

		public OptionSpecifier(string name, bool required = false, T defaultValue = default(T))
        {
            Name = name;
			Required = required;
			DefaultValue = defaultValue;
		}

		internal abstract Option<T> Parse(string value);
	}
}