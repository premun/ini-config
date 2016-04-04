using System;

namespace Config.Values
{
	public class ConstraintConfigValue<T> : ConfigValue where T : ConfigValue
	{
		public ConstraintConfigValue(ConfigValue value, Func<object,bool> constraint)
		{
			
			
		}
	}
}