using System;

namespace Config.Values
{
	public class ConstraintConfigValue<T> : ConfigValue where T : ConfigValue
	{
		public ConstraintConfigValue(T value, Func<object, bool> constraint)
		{
		}
	}
}