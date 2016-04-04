using System;

namespace Config.Values
{
	public class UnsignedConfigValue : ConfigValue<ulong>
	{
		public UnsignedConfigValue(ulong value)
		{
			Value = value;
		}

		public UnsignedConfigValue(string value)
		{
			throw new NotImplementedException();
		}

		public static implicit operator UnsignedConfigValue(ulong l)
		{
			return new UnsignedConfigValue(l);
		}
	}
}