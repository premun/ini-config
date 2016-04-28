using System.Collections.Generic;

namespace Config.Format
{
    public class ConfigFormatSpecifier
    {
	    internal IList<SectionSpecifier> Sections { get; }

	    public ConfigFormatSpecifier()
	    {
		    Sections = new List<SectionSpecifier>();
	    }

	    public FluentSectionSpecifier AddSection(string name, bool required = false)
	    {
			var sectionSpecifier = new SectionSpecifier(name, required);
			Sections.Add(sectionSpecifier);
		    return new FluentSectionSpecifier(this, sectionSpecifier);
	    }
	}
}