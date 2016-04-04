using System;
using System.Collections.Generic;

namespace Config.Values
{
	public class ListConfigValue<T> : ConfigValue where T : ConfigValue
	{
		public IEnumerable<T> Values { get; set; }

		public ListConfigValue(IEnumerable<T> values)
		{
			Values = values;
		}

		public ConfigValue this[int index]
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