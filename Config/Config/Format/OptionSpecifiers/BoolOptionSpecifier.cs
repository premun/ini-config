using Config.Options;

namespace Config.Format.OptionSpecifiers
{
	public class BoolOptionSpecifier : OptionSpecifier<bool>
	{
		public BoolOptionSpecifier(string name, bool required = false, bool defaultValue = false) : base(name, required, defaultValue)
		{
		}

		internal override Option<bool> Parse(string value)
		{
			return new BoolOption(value);
		}
	}
}
