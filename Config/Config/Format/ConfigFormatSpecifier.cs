using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ConfigTests")]

namespace Config.Format
{
    public class ConfigFormatSpecifier
    {
	    internal IDictionary<string, SectionSpecifier> Sections { get; }

	    public ConfigFormatSpecifier()
	    {
		    Sections = new Dictionary<string, SectionSpecifier>();
	    }

	    public FluentSectionSpecifier AddSection(string name, bool required = false)
	    {
			var sectionSpecifier = new SectionSpecifier(name, required);
			Sections[name] = sectionSpecifier;
		    return new FluentSectionSpecifier(this, sectionSpecifier);
	    }
	}
}