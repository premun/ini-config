using System;

namespace Config.Options
{
	public class ConstraintOption<T> : Option where T : Option
	{
		public ConstraintOption(T value, Func<object, bool> constraint)
		{
		}
	}
}