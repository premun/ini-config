using Config.Format.OptionSpecifiers;

namespace Config.Format
{
	public class FluentSectionSpecifier
	{
		private readonly ConfigFormatSpecifier _parentSpecifier;
		private readonly SectionSpecifier _sectionSpecifier;

		internal FluentSectionSpecifier(ConfigFormatSpecifier parentSpecifier, SectionSpecifier sectionSpecifier)
		{
			_parentSpecifier = parentSpecifier;
			_sectionSpecifier = sectionSpecifier;
		}

		public FluentSectionSpecifier AddSection(string name, bool required = false)
		{
			return _parentSpecifier.AddSection(name, required);
		}

		public FluentSectionSpecifier AddOption(OptionSpecifier optionSpecifier)
		{
			_sectionSpecifier.AddOption(optionSpecifier);
			return this;
		}

		public ConfigFormatSpecifier FinishDefinition()
		{
			return _parentSpecifier;
		}
	}
}