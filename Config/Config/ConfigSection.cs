using System.Collections;
using System.Collections.Generic;
using Config.Format;
using Config.Options;

namespace Config
{
	public class ConfigSection : IConfigSection
	{
		private readonly Dictionary<string, Option> _options;

		internal SectionSpecifier FormatSpecifier { get; set; }

		public string Name { get; }

		public string Comment { get; set; }

		public ConfigSection(string name)
		{
			Name = name;
			_options = new Dictionary<string, Option>();
		}

		public Option this[string key]
		{
			get {
				if (_options.ContainsKey(key))
				{
					return _options[key];
				}

				var specifier = FormatSpecifier[key];
				if (specifier != null && specifier.DefaultValue != null)
				{
					return specifier.Parse(specifier.DefaultValue);
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

		public bool Remove(string key)
		{
			return _options.Remove(key);
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