using System;

namespace Config.Options
{
    /// <summary>
    ///     Represents generic enum option.
    /// </summary>
    /// <typeparam name="T">Type of option enum.</typeparam>
    /// <seealso cref="Config.Options.Option{T}" />
    public sealed class EnumOption<T> : Option<T> where T : struct, IConvertible
    {
        internal T Value;

        public EnumOption(string value)
        {
            Data = (T) Enum.Parse(typeof (T), value);
        }

        public EnumOption(T value)
        {
            Value = value;
        }
    }
}