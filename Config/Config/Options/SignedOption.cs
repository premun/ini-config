using System;

namespace Config.Options
{
    /// <summary>
    ///     Represents long option.
    ///     Possible input formats:
    ///     <list type="number">
    ///         <item>0x... - Hexadecimal</item>
    ///         <item>0b - Binary</item>
    ///         <item>0 - Octal</item>
    ///     </list>
    /// </summary>
    /// <seealso cref="Config.Options.NumericOption{System.Int64}" />
    public sealed class SignedOption : NumericOption<long>
    {
        public SignedOption(long data)
        {
            Data = data;
        }

        public SignedOption(string value)
        {
            var preparedValue = ParseValueToNumeric(value);
            Data = Convert.ToInt64(preparedValue.StringValue,
                preparedValue.NumericSystem);
        }

        /// <summary>
        ///     Auto-boxing.
        ///     Performs an implicit conversion from <see cref="System.Int64" /> to <see cref="SignedOption" />.
        /// </summary>
        /// <param name="l">The l.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static implicit operator SignedOption(long l)
        {
            return new SignedOption(l);
        }
    }
}