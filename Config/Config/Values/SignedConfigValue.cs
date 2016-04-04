using System;

namespace Config.Values
{
	public class SignedConfigValue : ConfigValue<long>
	{
		public SignedConfigValue(long value)
		{
			Value = value;
		}

		public SignedConfigValue(string value)
		{
			throw new NotImplementedException();
		}

		public static implicit operator SignedConfigValue(long l)
		{
			return new SignedConfigValue(l);
		}
	}
}