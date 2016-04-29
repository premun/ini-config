using Config.Options;

namespace Config.Format.OptionSpecifiers
{
	public class SignedOptionSpecifier : OptionSpecifier<long>
	{
		public SignedOptionSpecifier(string name, bool required = false, long defaultValue = 0) : base(name, required, defaultValue)
		{
		}

		internal override Option<long> Parse(string value)
		{
			return new SignedOption(value);
		}
	}
}
