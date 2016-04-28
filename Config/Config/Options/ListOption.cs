using System;
using System.Collections.Generic;

namespace Config.Options
{
	public class ListOption<T> : Option where T : Option
	{
		public IEnumerable<T> Values { get; set; }

		public ListOption(IEnumerable<T> values)
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

		public List<T> Get()
		{
			throw new NotImplementedException();
		}
	}
}