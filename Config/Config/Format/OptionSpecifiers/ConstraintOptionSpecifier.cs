using System;
using Config.Format.Errors;
using Config.Options;

namespace Config.Format.OptionSpecifiers
{
	public class ConstraintOptionSpecifier<T> : OptionSpecifier<T>
	{
		public readonly Predicate<T> Constraint;

		public ConstraintOptionSpecifier(string name, Predicate<T> constraint,  bool required = false, T defaultValue = default(T)) : base(name, required, defaultValue)
		{
			Constraint = constraint;
		}

		internal override Option Parse(string value)
		{
			// TODO: catch exception, throw wrong type exception
			var val = (T) Convert.ChangeType(value, typeof(T));

			if (Constraint(val))
			{
				return new ConstraintOption<T>(val, Constraint);	
			}

			throw new InvalidValueException(string.Format("Option {0} value ({1}) is not within allowed bounds.", Name, value));
		}
	}
}