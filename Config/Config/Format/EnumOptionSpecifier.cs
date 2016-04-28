using System;

namespace Config.Format
{
	internal class EnumOptionSpecifier : OptionSpecifier
	{
		internal Type Enumeration { get; set; }

		internal EnumOptionSpecifier(string name, Type enumeration, bool required = false, object defaultValue = null)
			: base(name, required, defaultValue)
		{
			Enumeration = enumeration;
		}
	}
}
