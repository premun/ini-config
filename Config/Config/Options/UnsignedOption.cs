using System;

namespace Config.Options
{
    /// <summary>
    ///     Represents unsigned long option.
    ///     Possible input formats:
    ///     <list type="number">
    ///         <item>0x... - Hexadecimal</item>
    ///         <item>0b - Binary</item>
    ///         <item>0 - Octal</item>
    ///     </list>
    /// </summary>
    /// <seealso cref="Config.Options.NumericOption{System.UInt64}" />
    public sealed class UnsignedOption : NumericOption<ulong>
    {
        public UnsignedOption(ulong data)
        {
            Data = data;
        }

        public UnsignedOption(string value)
        {
            var preparedValue = ParseValueToNumeric(value);
            Data = Convert.ToUInt64(preparedValue.StringValue,
                preparedValue.NumericSystem);
        }

        /// <summary>
        ///     Auto-boxing.
        ///     Performs an implicit conversion from <see cref="System.UInt64" /> to <see cref="UnsignedOption" />.
        /// </summary>
        /// <param name="l">The l.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static implicit operator UnsignedOption(ulong l)
        {
            return new UnsignedOption(l);
        }
    }
}