using Config.Options;

namespace Config.Format.OptionSpecifiers
{
    /// <summary>
    ///     Represents the format specification for an boolean option. The specifier uses a null-able type for better data
    ///     manipulation (specially with default value).
    /// </summary>
    /// <seealso cref="Config.Format.OptionSpecifiers.OptionSpecifier{System.Boolean?}" />
    public sealed class BoolOptionSpecifier : OptionSpecifier<bool?>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BoolOptionSpecifier" /> class.
        /// </summary>
        /// <param name="name">The option name.</param>
        /// <param name="required">if set to <c>true</c> [required].</param>
        /// <param name="defaultValue">The default value.</param>
        public BoolOptionSpecifier(string name, bool required = false,
            bool? defaultValue = null)
            : base(name, required, defaultValue)
        {
        }

        /// <summary>
        ///     Parses the specified value to the boolean option (creates new instance of
        ///     <seealso cref="Config.Options.BoolOption" />).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     Parsed boolean option value.
        /// </returns>
        internal override Option Parse(string value)
        {
            return new BoolOption(value);
        }
    }
}