using System;

namespace Config.Options
{
    public static class NumericPrefix
    {
        public const string Hex = "0x";
        public const string Binary = "0b";
        public const string Octal = "0";
    }

    public abstract class NumericOption<T> : Option<T>
    {
        public NumericValue ParseValueToNumeric(string value)
        {
            if (value.Length > 1 && value.StartsWith("0"))
            {
                // Checks if value is hexadecimal
                if (value.StartsWith(NumericPrefix.Hex))
                {
                    return new HexValue(value);
                }
                // Checks if value is binary
                if (value.StartsWith(NumericPrefix.Binary))
                {
                    return new BinaryValue(value);
                }
                if (value.StartsWith(NumericPrefix.Octal))
                {
                    return new OctalValue(value);
                }
            }

            return new DecimalValue(value);
        }
    }

    public abstract class NumericValue
    {
        public abstract string StringValue { get; }

        public abstract int NumericSystem { get; }
    }

    public class DecimalValue : NumericValue
    {
        private readonly string _value;

        public DecimalValue(string value)
        {
            _value = value;
        }

        #region Overrides of NumericValue<int>

        public override string StringValue
        {
            get { return _value; }
        }

        public override int NumericSystem
        {
            get { return 10; }
        }

        #endregion
    }

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