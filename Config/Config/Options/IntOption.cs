using System;

namespace Config.Options
{
    public sealed class IntOption : NumericOption<int>
	{
		public IntOption(int data)
		{
			Data = data;
		}

		public IntOption(string value)
		{
            var preparedValue = ParseValueToNumeric(value);
            Data = Convert.ToInt32(preparedValue.StringValue, preparedValue.NumericSystem);
        }

		public static implicit operator IntOption(int i)
		{
			return new IntOption(i);
		}
	}
}
