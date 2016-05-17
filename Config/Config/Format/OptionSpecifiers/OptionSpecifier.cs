using System.Globalization;
using Config.Options;

namespace Config.Format.OptionSpecifiers
{
    /// <summary>
    ///     Parent object for option specifiers. Specifiers parse given data to the required type.
    /// </summary>
    public abstract class OptionSpecifier
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="OptionSpecifier" /> class.
        /// </summary>
        /// <param name="name">The name of the option.</param>
        /// <param name="required">if set to <c>true</c> [required].</param>
        /// <param name="defaultValue">The default value of the option.</param>
        protected OptionSpecifier(string name, bool required = false,
            object defaultValue = null)
        {
            Name = name;
            Required = required;
            DefaultValue = defaultValue;
        }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name of the option.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="OptionSpecifier" /> represents a required option.
        /// </summary>
        /// <value>
        ///     <c>true</c> if required; otherwise, <c>false</c>.
        /// </value>
        public bool Required { get; set; }

        /// <summary>
        ///     Gets or sets the default value of the option.
        /// </summary>
        /// <value>
        ///     The default value.
        /// </value>
        public object DefaultValue { get; set; }

        /// <summary>
        ///     Parses the specified value to the option.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Parsed option value.</returns>
        internal abstract Option Parse(string value);

        /// <summary>
        ///     Parses the specified value to the option.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Parsed option value.</returns>
        internal Option Parse(object value)
        {
            return
                Parse(string.Format(CultureInfo.InvariantCulture, "{0}", value));
        }
    }

    /// <summary>
    ///     Extends the <see cref="OptionSpecifier" /> with generic type of data.
    /// </summary>
    /// <typeparam name="T">Type of option data.</typeparam>
    public abstract class OptionSpecifier<T> : OptionSpecifier
    {
        protected OptionSpecifier(string name, bool required, T defaultValue)
            : base(name, required, defaultValue)
        {
        }
    }
}