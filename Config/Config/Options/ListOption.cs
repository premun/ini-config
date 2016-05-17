using System.Collections.Generic;
using System.Linq;

namespace Config.Options
{
    /// <summary>
    ///     Represents list of options based on generic type T.
    /// </summary>
    /// <typeparam name="T">The type of inner options.</typeparam>
    /// <seealso cref="Config.Options.Option{T}" />
    public sealed class ListOption<T> : Option<T> where T : Option
    {
        private IList<T> _values;

        public ListOption(IEnumerable<T> values)
        {
            Values = values;
        }

        public ListOption(T[] values)
        {
            Values = values;
        }

        public IEnumerable<T> Values
        {
            get { return _values; }
            set { _values = value.ToList(); }
        }

        #region Overrides of Option

        public override object Data
        {
            get { return Values; }
            protected set { Values = (IEnumerable<T>) value; }
        }

        #endregion

        public IEnumerable<T> Get()
        {
            return _values;
        }

        public override string Serialize()
        {
            return string.Join(", ", _values.Select(x => x.Serialize()));
        }

        /// <summary>
        ///     Auto-boxing.
        ///     Performs an implicit conversion from <see cref="T[]" /> to <see cref="ListOption{T}" />.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        public static implicit operator ListOption<T>(T[] t)
        {
            return new ListOption<T>(t);
        }
    }
}