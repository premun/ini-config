namespace Config.Options
{
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
}