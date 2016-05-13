using System;

namespace Config.Options
{
    public class UnsignedOption : NumericOption<ulong>
	{
		public UnsignedOption(ulong data)
		{
			Data = data;
		}

		public UnsignedOption(string value)
		{
            var preparedValue = ParseValueToNumeric(value);
            Data = Convert.ToUInt64(preparedValue.StringValue, preparedValue.NumericSystem);
		}

		public static implicit operator UnsignedOption(ulong l)
		{
			return new UnsignedOption(l);
		}
	}
}