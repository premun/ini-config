using System;

namespace Config.Values
{
	public class FloatConfigValue : ConfigValue<float>
	{
		public FloatConfigValue(float value)
		{
			Value = value;
		}

		public FloatConfigValue(string value)
		{
			throw new NotImplementedException();
		}

		public static implicit operator FloatConfigValue(float f)
		{
			return new FloatConfigValue(f);
		}
	}
}
