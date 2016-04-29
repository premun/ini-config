using System;
using Config.Options;

namespace Config.Format.OptionSpecifiers
{
	public class EnumOptionSpecifier<T> : OptionSpecifier<T> where T : struct, IConvertible
	{
		public EnumOptionSpecifier(string name, bool required = false, T defaultValue = default(T)) : base(name, required, defaultValue)
		{
		}

		internal override Option Parse(string value)
		{
			return new EnumOption<T>(value);
		}
	}
}
