using Config.Options;

namespace Config.Format.OptionSpecifiers
{
    /// <summary>
    ///     Represents the format specification for an unsigned long option. The specifier uses a null-able type for better
    ///     data manipulation (specially with default value).
    /// </summary>
    /// <seealso cref="Config.Format.OptionSpecifiers.OptionSpecifier{System.UInt64?}" />
    public sealed class UnsignedOptionSpecifier : OptionSpecifier<ulong?>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UnsignedOptionSpecifier" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="required">if set to <c>true</c> [required].</param>
        /// <param name="defaultValue">The default value.</param>
        public UnsignedOptionSpecifier(string name, bool required = false,
            ulong? defaultValue = null)
            : base(name, required, defaultValue)
        {
        }

        /// <summary>
        ///     Parses the specified value to the unsigned long option (creates new instance of
        ///     <seealso cref="Config.Options.UnsignedOption" />).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     Parsed option value.
        /// </returns>
        internal override Option Parse(string value)
        {
            return new UnsignedOption(value);
        }
    }
}