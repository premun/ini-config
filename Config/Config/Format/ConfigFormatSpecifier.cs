using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ConfigTests")]

namespace Config.Format
{
    public class ConfigFormatSpecifier
	{
	    private readonly IDictionary<string, SectionSpecifier> _sections;

		internal SectionSpecifier this[string key]
		{
			get
			{
				if (_sections.ContainsKey(key))
				{
					return _sections[key];
				}

				return null;
			}

			set { _sections[key] = value; }
		}

		public ConfigFormatSpecifier()
	    {
		    _sections = new Dictionary<string, SectionSpecifier>();
	    }

	    public FluentSectionSpecifier AddSection(string name, bool required = false)
	    {
			var sectionSpecifier = new SectionSpecifier(name, required);
			_sections[name] = sectionSpecifier;
		    return new FluentSectionSpecifier(this, sectionSpecifier);
	    }
	}
}