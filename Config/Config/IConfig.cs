using System.Collections.Generic;
using Config.Format;

namespace Config
{
    public interface IConfig
    {
        IConfigSection this[string key] { get; }
	    IConfigSection GetSection(string key);

		IEnumerable<IConfigSection> Sections { get; }
		bool AddSection(IConfigSection section);
		bool RemoveSection(IConfigSection section);
		bool RemoveSection(string name);

		void Save(string path = null);
    }
}