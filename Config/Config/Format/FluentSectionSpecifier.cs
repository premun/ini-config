using System;

namespace Config.Format
{
	public class FluentSectionSpecifier
	{
		private readonly ConfigFormatSpecifier _parentSpecifier;
		private readonly SectionFormatSpecifier _sectionSpecifier;

		internal FluentSectionSpecifier(ConfigFormatSpecifier parentSpecifier, SectionFormatSpecifier sectionSpecifier)
		{
			_parentSpecifier = parentSpecifier;
			_sectionSpecifier = sectionSpecifier;
		}

		public FluentSectionSpecifier AddSection(string name, bool required = false)
		{
			return _parentSpecifier.AddSection(name, required);
		}

		public FluentSectionSpecifier AddOption(string name, bool required = false, object defaultValue = null)
		{
			_sectionSpecifier.AddOption(name, required, defaultValue);
			return this;
		}

		public FluentSectionSpecifier AddOption(string name, Predicate<object> constraint, bool required = false, object defaultValue = null)
		{
			_sectionSpecifier.AddOption(name, constraint, required, defaultValue);
			return this;
		}

		public FluentSectionSpecifier AddOption(string name, Type enumeration, bool required = false, object defaultValue = null)
		{
			_sectionSpecifier.AddOption(name, enumeration, required, defaultValue);
			return this;
		}

		public ConfigFormatSpecifier FinishDefinition()
		{
			return _parentSpecifier;
		}
	}
}