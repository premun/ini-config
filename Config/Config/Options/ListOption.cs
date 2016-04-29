using System;
using System.Collections.Generic;

namespace Config.Options
{
	public class ListOption<T> : Option<T> where T : Option
	{
		public IEnumerable<T> Values { get; set; }

		public ListOption(IEnumerable<T> values)
		{
			Values = values;
		}

		public ListOption(T[] values)
		{
			Values = values;
		}

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

		public static implicit operator ListOption<T>(T[] t)
		{
			return new ListOption<T>(t);
		}
	}
}