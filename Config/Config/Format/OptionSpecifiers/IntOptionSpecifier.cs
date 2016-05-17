using Config.Options;

namespace Config.Format.OptionSpecifiers
{
    /// <summary>
    ///     Represents the format specification for an integer option. The specifier uses a null-able type for better data
    ///     manipulation (specially with default value).
    /// </summary>
    /// <seealso cref="Config.Format.OptionSpecifiers.OptionSpecifier{System.Int32?}" />
    public sealed class IntOptionSpecifier : OptionSpecifier<int?>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="IntOptionSpecifier" /> class.
        /// </summary>
        /// <param name="name">The name of the option.</param>
        /// <param name="required">if set to <c>true</c> [required].</param>
        /// <param name="defaultValue">The default value.</param>
        public IntOptionSpecifier(string name, bool required = false,
            int? defaultValue = null)
            : base(name, required, defaultValue)
        {
        }

        /// <summary>
        ///     Parses the specified value to the boolean option (creates new instance of
        ///     <seealso cref="Config.Options.IntOption" />).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     Parsed option value.
        /// </returns>
        internal override Option Parse(string value)
        {
            return new IntOption(value);
        }
    }
}