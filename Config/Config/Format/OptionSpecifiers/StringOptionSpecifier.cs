using Config.Options;

namespace Config.Format.OptionSpecifiers
{
	public class StringOptionSpecifier : OptionSpecifier<string>
	{
		public StringOptionSpecifier(string name, bool required = false, string defaultValue = null) : base(name, required, defaultValue)
		{
		}

		internal override Option<string> Parse(string value)
		{
			return new StringOption(value);
		}
	}
}
