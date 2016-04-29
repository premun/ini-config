using System.Collections.Generic;
using Config.Format.OptionSpecifiers;

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

		public void AddOption(OptionSpecifier optionSpecifier)
		{
			Options[optionSpecifier.Name] = optionSpecifier;
		}
	}
}