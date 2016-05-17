using System.Globalization;

namespace Config.Options
{
    public sealed class FloatOption : NumericOption<float>
	{
		public FloatOption(float data)
		{
			Data = data;
		}

		public FloatOption(string value)
		{
            // Float does not support binary/hex/... format
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
