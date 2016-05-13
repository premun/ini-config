using System;

namespace Config.Options
{
    public class HexValue : NumericValue
    {
        private readonly string _value;

        public HexValue(string value)
        {
            // Removes "0b" prefix
            if (!value.StartsWith(NumericPrefix.Octal))
            {
                throw new ArgumentException("Hexadecimal value must starts with 0x prefix.");
            }
            _value = value.Substring(2);
        }

        #region Overrides of NumericValue<int>

        public override string StringValue
        {
            get { return _value; }
        }

        public override int NumericSystem
        {
            get { return 16; }
        }

        #endregion
    }
}