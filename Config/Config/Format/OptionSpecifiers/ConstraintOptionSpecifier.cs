using System;
using Config.Options;

namespace Config.Format.OptionSpecifiers
{
    /// <summary>
    ///     Represents the format specification for a constraint option. Constraint is validated during a parsing of the given
    ///     value.
    /// </summary>
    /// <typeparam name="T">The type of the option.</typeparam>
    /// <seealso cref="Config.Format.OptionSpecifiers.OptionSpecifier{T}" />
    public sealed class ConstraintOptionSpecifier<T> : OptionSpecifier<T>
    {
        private readonly Predicate<T> _constraint;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConstraintOptionSpecifier{T}" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="constraint">The constraint.</param>
        /// <param name="required">if set to <c>true</c> [required].</param>
        /// <param name="defaultValue">The default value.</param>
        public ConstraintOptionSpecifier(string name, Predicate<T> constraint,
            bool required = false, T defaultValue = default(T))
            : base(name, required, defaultValue)
        {
            _constraint = constraint;
        }

        /// <summary>
        ///     Parses the specified value to the constraint option (creates new instance of
        ///     <seealso cref="Config.Options.ConstraintOption{T}" />).
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///     Parsed option value.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        ///     The exception is thrown when the constraint is not within allowed
        ///     bounds.
        /// </exception>
        internal override Option Parse(string value)
        {
            T val;

            // Enum has to be parsed (cannot be converted)
            if (default(T) is Enum)
            {
                val = (T) Enum.Parse(typeof (T), value);
            }
            else
            {
                val = (T) Convert.ChangeType(value, typeof (T));
            }

            if (_constraint(val))
            {
                return new ConstraintOption<T>(val, _constraint);
            }

            throw new ArgumentOutOfRangeException(
                string.Format(
                    "Option {0} value ({1}) is not within allowed bounds.", Name,
                    value));
        }
    }
}