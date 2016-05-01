using System.Collections.Generic;
using Config.Format;

namespace Config
{
	// TODO: Jenom rychla implementace, aby mi bezely testy. Treba asi udelat poradne.
	public class Config : IConfig
	{
		private readonly Dictionary<string, IConfigSection> _sections;

		internal Config(ConfigFormatSpecifier formatSpecifier = null)
		{
			FormatSpecifier = formatSpecifier;
			_sections = new Dictionary<string, IConfigSection>();
		}

		public ConfigFormatSpecifier FormatSpecifier { get; }

		public IConfigSection this[string name]
		{
			get
			{
				if (_sections.ContainsKey(name))
				{
					return _sections[name];
				}

				return null;
			}
		}

		public IEnumerable<IConfigSection> Sections
		{
		    get { return _sections.Values; }
		}

	    public bool AddSection(IConfigSection section)
	    {
		    bool sectionExisted = _sections.ContainsKey(section.Name);
		    _sections[section.Name] = section;
		    return sectionExisted;
	    }

		public IConfigSection AddSection(string name)
		{
			var section = new ConfigSection(name);
			_sections[name] = section;
			return section;
		}

		public bool RemoveSection(IConfigSection section)
		{
			bool sectionExisted = _sections.ContainsKey(section.Name);
			_sections.Remove(section.Name);
			return sectionExisted;
		}

		public bool RemoveSection(string name)
		{
			bool sectionExisted = _sections.ContainsKey(name);
			_sections.Remove(name);
			return sectionExisted;
		}
	}
}