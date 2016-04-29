using Config.Options;

namespace Config.Format.OptionSpecifiers
{
	public class IntOptionSpecifier : OptionSpecifier<int>
	{
		public IntOptionSpecifier(string name, bool required = false, int defaultValue = 0) : base(name, required, defaultValue)
		{
		}

		internal override Option Parse(string value)
		{
			return new IntOption(value);
		}
	}
}
