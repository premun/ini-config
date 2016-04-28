using System.Collections.Generic;

namespace Config.Format
{
    public class ConfigFormatSpecifier
    {
	    internal IList<SectionFormatSpecifier> Sections { get; }

	    public ConfigFormatSpecifier()
	    {
		    Sections = new List<SectionFormatSpecifier>();
	    }

	    public FluentSectionSpecifier AddSection(string name, bool required = false)
	    {
			var sectionSpecifier = new SectionFormatSpecifier(name, required);
			Sections.Add(sectionSpecifier);
		    return new FluentSectionSpecifier(this, sectionSpecifier);
	    }
	}
}