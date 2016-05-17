using Config.Options;

namespace Config.Format.OptionSpecifiers
{
    /// <summary>
    ///     Represents the format specification for a float option. The specifier uses a null-able type for better data
    ///     manipulation (specially with default value).
    /// </summary>
    /// <seealso cref="Config.Format.OptionSpecifiers.OptionSpecifier{System.Single?}" />
    public sealed class FloatOptionSpecifier : OptionSpecifier<float?>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FloatOptionSpecifier" /> class.
        /// </summary>
        /// <param name="name">The name of the option.</param>
        /// <param name="required">if set to <c>true</c> [required].</param>
        /// <param name="defaultValue">The default value.</param>
        public FloatOptionSpecifier(string name, bool required = false,
            float? defaultValue = null)
            : base(name, required, defaultValue)
        {
        }

        /// <summary>
        ///     Parses the specified value to the boolean option (creates new instance of
        ///     <seealso cref="Config.Options.FloatOption" />).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     Parsed option value.
        /// </returns>
        internal override Option Parse(string value)
        {
            return new FloatOption(value);
        }
    }
}