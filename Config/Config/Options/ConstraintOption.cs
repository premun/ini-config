using System;

namespace Config.Options
{
    /// <summary>
    ///     Represents option with constrains.
    /// </summary>
    /// <typeparam name="T">Type of inner option.</typeparam>
    /// <seealso cref="Config.Options.Option{T}" />
    public sealed class ConstraintOption<T> : Option<T>
    {
        public ConstraintOption(T value, Predicate<T> constraint)
        {
            Data = value;
            Constraint = constraint;
        }

        internal Predicate<T> Constraint { get; set; }
    }
}