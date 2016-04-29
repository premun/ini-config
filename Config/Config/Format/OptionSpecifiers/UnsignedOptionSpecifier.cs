using Config.Options;

namespace Config.Format.OptionSpecifiers
{
	public class UnsignedOptionSpecifier : OptionSpecifier<ulong>
	{
		public UnsignedOptionSpecifier(string name, bool required = false, ulong defaultValue = 0) : base(name, required, defaultValue)
		{
		}

		internal override Option Parse(string value)
		{
			return new UnsignedOption(value);
		}
	}
}
