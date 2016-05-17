using Config.Options;

namespace Config.Format.OptionSpecifiers
{
    /// <summary>
    ///     Represents the format specification for a long option. The specifier uses a null-able type for better data
    ///     manipulation (specially with default value).
    /// </summary>
    /// <seealso cref="Config.Format.OptionSpecifiers.OptionSpecifier{System.Int64?}" />
    public sealed class SignedOptionSpecifier : OptionSpecifier<long?>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SignedOptionSpecifier" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="required">if set to <c>true</c> [required].</param>
        /// <param name="defaultValue">The default value.</param>
        public SignedOptionSpecifier(string name, bool required = false,
            long? defaultValue = null)
            : base(name, required, defaultValue)
        {
        }

        /// <summary>
        ///     Parses the specified value to the long option (creates new instance of
        ///     <seealso cref="Config.Options.SignedOption" />).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     Parsed option value.
        /// </returns>
        internal override Option Parse(string value)
        {
            return new SignedOption(value);
        }
    }
}