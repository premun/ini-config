using Config.Options;

namespace Config.Format.OptionSpecifiers
{
	public class FloatOptionSpecifier : OptionSpecifier<float?>
	{
		public FloatOptionSpecifier(string name, bool required = false, float? defaultValue = null) 
			: base(name, required, defaultValue)
		{
		}

		internal override Option Parse(string value)
		{
			return new FloatOption(value);
		}
	}
}
