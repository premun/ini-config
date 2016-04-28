using System;
using System.Collections.Generic;

namespace Config.Format
{
	internal class SectionSpecifier
	{
		internal IDictionary<string, OptionSpecifier> Options { get; set; }

		internal string Name { get; set; }

		internal bool Required { get; set; }

		internal SectionSpecifier(string name, bool required = false)
		{
			Name = name;
			Required = required;
			Options = new Dictionary<string, OptionSpecifier>();
		}

		public void AddOption(string name, bool required = false, object defaultValue = null)
		{
			Options[name] = new OptionSpecifier(name, required, defaultValue);
		}

		public void AddOption(string name, Predicate<object> constraint, bool required = false, object defaultValue = null)
		{
			Options[name] = new ConstraintOptionSpecifier(name, constraint, required, defaultValue);
		}

		public void AddOption(string name, Type enumeration, bool required = false, object defaultValue = null)
		{
			Options[name] = new EnumOptionSpecifier(name, enumeration, required, defaultValue);
		}
	}
}