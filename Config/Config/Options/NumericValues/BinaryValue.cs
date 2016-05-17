using System;

namespace Config.Options.NumericValues
{
    public class BinaryValue : NumericValue
    {
        private readonly string _value;

        public BinaryValue(string value)
        {
            // Removes "0b" prefix
            if (!value.StartsWith(NumericPrefix.Binary))
            {
                throw new ArgumentException("Binary value must starts with 0b prefix."); 
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
            get { return 2; }
        }

        #endregion
    }
}