using System;

namespace Config.Options
{
    public class OctalValue : NumericValue
    {
        private readonly string _value;

        public OctalValue(string value)
        {
            // Removes "0b" prefix
            if (!value.StartsWith(NumericPrefix.Octal))
            {
                throw new ArgumentException("Octal value must starts with 0 prefix.");
            }
            _value = value.Substring(1);
        }

        #region Overrides of NumericValue<int>

        public override string StringValue
        {
            get { return _value; }
        }

        public override int NumericSystem
        {
            get { return 8; }
        }

        #endregion
    }
}