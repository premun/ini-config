using System.Collections.Generic;

namespace Config.Values
{
	public abstract class ListConfigValue<T> : ConfigValue where T : ConfigValue
	{
		public abstract List<T> Get();
	}
}