using System;
using System.Globalization;

namespace Config.Options
{
	public class FloatOption : Option<float>
	{
		public FloatOption(float data)
		{
			Data = data;
		}

		public FloatOption(string value)
		{
			Data = float.Parse(value, CultureInfo.InvariantCulture);
		}

		public override string Serialize()
		{
			return ((float)Data).ToString(CultureInfo.InvariantCulture);
		}

		public static implicit operator FloatOption(float f)
		{
			return new FloatOption(f);
		}
	}
}
