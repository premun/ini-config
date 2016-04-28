using System;

namespace Config.Options
{
	public class FloatOption : Option<float>
	{
		public FloatOption(float rawValue)
		{
			RawValue = rawValue;
		}

		public FloatOption(string value)
		{
			throw new NotImplementedException();
		}

		public static implicit operator FloatOption(float f)
		{
			return new FloatOption(f);
		}
	}
}
