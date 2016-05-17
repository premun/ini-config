using Config.Options;

namespace Config.Format.OptionSpecifiers
{
    /// <summary>
    ///     Represents the format specification for a text option. The specifier uses a null-able type for better data
    ///     manipulation (specially with default value).
    /// </summary>
    /// <seealso cref="Config.Format.OptionSpecifiers.OptionSpecifier{System.String}" />
    public sealed class StringOptionSpecifier : OptionSpecifier<string>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="StringOptionSpecifier" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="required">if set to <c>true</c> [required].</param>
        /// <param name="defaultValue">The default value.</param>
        public StringOptionSpecifier(string name, bool required = false,
            string defaultValue = null) : base(name, required, defaultValue)
        {
        }

        /// <summary>
        ///     Parses the specified value to the text option (creates new instance of
        ///     <seealso cref="Config.Options.StringOption" />).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     Parsed option value.
        /// </returns>
        internal override Option Parse(string value)
        {
            return new StringOption(value);
        }
    }
}