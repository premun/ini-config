using System;
using System.Collections.Generic;
using System.Linq;

namespace Config.Options
{
    public class ListOption<T> : Option<T> where T : Option
    {
        private IList<T> _values;

        public IEnumerable<T> Values
        {
            get { return _values; }
            set { _values = value.ToList(); }
        }

        #region Overrides of Option

        public override object Data
        {
            get
            {
                return Values;
            }
            protected set
            {
                Values = (IEnumerable<T>)value;
            }
        }

        #endregion

        public ListOption(IEnumerable<T> values)
        {
            Values = values;
        }

        public ListOption(T[] values)
        {
            Values = values;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListOption{T}"/> class.
        /// This constructor is used for runtime generic initialization.
        /// </summary>
        public ListOption() { }

        public Option this[int index]
        {
            get
            {
                return _values[index];
            }

            set { _values[index] = (T) value; }
        }

        public IEnumerable<T> Get()
        {
            return _values;
        }

        public override string Serialize()
        {
            // TODO
            throw new NotImplementedException();
        }

        public static implicit operator ListOption<T>(T[] t)
        {
            return new ListOption<T>(t);
        }
    }
}