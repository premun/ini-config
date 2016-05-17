using System;

namespace Config.Options
{
    /// <summary>
    ///     Represents integer option.
    ///     Possible input formats:
    ///     <list type="number">
    ///         <item>0x... - Hexadecimal</item>
    ///         <item>0b - Binary</item>
    ///         <item>0 - Octal</item>
    ///     </list>
    /// </summary>
    /// <seealso cref="Config.Options.NumericOption{System.Int32}" />
    public sealed class IntOption : NumericOption<int>
    {
        public IntOption(int data)
        {
            Data = data;
        }

        public IntOption(string value)
        {
            var preparedValue = ParseValueToNumeric(value);
            Data = Convert.ToInt32(preparedValue.StringValue,
                preparedValue.NumericSystem);
        }

        /// <summary>
        ///     Auto-boxing.
        ///     Performs an implicit conversion from <see cref="System.Int32" /> to <see cref="IntOption" />.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static implicit operator IntOption(int i)
        {
            return new IntOption(i);
        }
    }
}