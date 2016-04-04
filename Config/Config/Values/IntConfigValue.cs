using System;

namespace Config.Values
{
	public class IntConfigValue : ConfigValue<int>
	{
		public IntConfigValue(int value)
		{
			Value = value;
		}

		public IntConfigValue(string value)
		{
			throw new NotImplementedException();
		}

		public static implicit operator IntConfigValue(int i)
		{
			return new IntConfigValue(i);
		}
	}
}
