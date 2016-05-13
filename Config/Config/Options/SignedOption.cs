using System;

namespace Config.Options
{
    public sealed class SignedOption : NumericOption<long>
    {
        public SignedOption(long data)
        {
            Data = data;
        }

        public SignedOption(string value)
        {
            var preparedValue = ParseValueToNumeric(value);
            Data = Convert.ToInt64(preparedValue.StringValue, preparedValue.NumericSystem);
        }

        public static implicit operator SignedOption(long l)
        {
            return new SignedOption(l);
        }
    }
}