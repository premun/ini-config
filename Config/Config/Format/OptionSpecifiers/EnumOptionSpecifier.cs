using System;
using Config.Options;

namespace Config.Format.OptionSpecifiers
{
    /// <summary>
    ///     Represents the format specification for an enum option.
    /// </summary>
    /// <typeparam name="T">The specific type of option enum.</typeparam>
    /// <seealso cref="Config.Format.OptionSpecifiers.OptionSpecifier{T}" />
    public sealed class EnumOptionSpecifier<T> : OptionSpecifier<T>
        where T : struct, IConvertible
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EnumOptionSpecifier{T}" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="required">if set to <c>true</c> [required].</param>
        /// <param name="defaultValue">The default value.</param>
        public EnumOptionSpecifier(string name, bool required = false,
            T defaultValue = default(T))
            : base(name, required, defaultValue)
        {
        }

        /// <summary>
        ///     Parses the specified value to the enum option (creates new instance of
        ///     <seealso cref="Config.Options.EnumOption{T}" />).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     Parsed option value.
        /// </returns>
        internal override Option Parse(string value)
        {
            return new EnumOption<T>(value);
        }
    }
}