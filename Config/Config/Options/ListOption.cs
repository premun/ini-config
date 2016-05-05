using System;
using System.Collections.Generic;

namespace Config.Options
{
    public class ListOption<T> : Option<T> where T : Option
    {
        public IEnumerable<T> Values { get; set; }

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

        public ListOption() { }

        // TODO
        public Option this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<T> Get()
        {
            throw new NotImplementedException();
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