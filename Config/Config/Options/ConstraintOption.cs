using System;

namespace Config.Options
{
	public class ConstraintOption<T> : Option<T>
	{
		public ConstraintOption(T value, Predicate<T> constraint)
		{
		}
	}
}