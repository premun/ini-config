using System.Collections;
using System.Collections.Generic;
using Config.Options;

namespace Config
{
	public class ConfigSection : IConfigSection
	{
		private readonly Dictionary<string, Option> _options;

		public ConfigSection(string name)
		{
			Name = name;
			_options = new Dictionary<string, Option>();
		}

		public string Name { get; }

		public string Comment { get; set; }

		public Option this[string key]
		{
			get {
				if (_options.ContainsKey(key))
				{
					return _options[key];
				}

				return null;
			}

			set
			{
				_options[key] = value;
			}
		}

		public IConfigSection Set(string key, Option value)
		{
			this[key] = value;
			return this;
		}

		public IEnumerator<Option> GetEnumerator()
		{
			return _options.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}