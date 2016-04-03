using System;
using System.Collections.Generic;

namespace Config.Values
{
	public abstract class ListConfigValue<T> : ConfigValue where T : ConfigValue
	{
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

		public abstract List<T> Get();
	}
}