using System.Collections.Generic;
using Config.Format;

namespace Config
{
	public class Config : IConfig
	{
		private readonly Dictionary<string, IConfigSection> _sections;

		internal Config(ConfigFormatSpecifier formatSpecifier = null)
		{
			FormatSpecifier = formatSpecifier;
			_sections = new Dictionary<string, IConfigSection>();
		}

        public ConfigFormatSpecifier FormatSpecifier { get; private set; }

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

			// New section needs to know its specification in order to parse values / retrieve defaults
			if (FormatSpecifier != null && FormatSpecifier[name] != null)
			{
				section.FormatSpecifier = FormatSpecifier[name];
			}

			_sections[name] = section;
			return section;
		}

		public bool RemoveSection(IConfigSection section)
		{
			return _sections.Remove(section.Name); 
		}

		public bool RemoveSection(string name)
		{
			return _sections.Remove(name);
		}
	}
}