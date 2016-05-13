using System;

namespace Config.Options
{
	public sealed class ConstraintOption<T> : Option<T>
	{
		internal Predicate<T> Constraint { get; set; }

		public ConstraintOption(T value, Predicate<T> constraint)
		{
			Data = value;
			Constraint = constraint;
		}
	}
}