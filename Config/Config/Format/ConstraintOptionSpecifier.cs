using System;

namespace Config.Format
{
	internal class ConstraintOptionSpecifier : OptionSpecifier
	{
		internal Predicate<object> Constraint { get; set; }

		internal ConstraintOptionSpecifier(string name, Predicate<object> constraint, bool required = false, object defaultValue = null)
			: base(name, required, defaultValue)
		{
			Constraint = constraint;
		}
    }
}
