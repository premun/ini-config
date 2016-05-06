using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Config.Format;
using Config.Options;

namespace Config
{
	/// <summary>
	/// Represents a section of config containing config options.
	/// </summary>
	public class ConfigSection : IConfigSection
	{
		private readonly Dictionary<string, Option> _options;

		internal SectionSpecifier FormatSpecifier { get; set; }

        public string Name { get; private set; }

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

				// Option not present, but a default value might be provided
				var specifier = FormatSpecifier[key];
				if (specifier != null && specifier.DefaultValue != null)
				{
					return specifier.Parse(specifier.DefaultValue);
				}

				return null;
			}

			set
			{
                if (_options.ContainsKey(key) && _options[key].GetType() == typeof(ReferenceOption))
			    {
                    throw new InvalidOperationException("Cannot set value to reference option.");
			    }
				_options[key] = value;
			}
		}

		public IEnumerable<string> Keys(bool keysOfDefaults = true)
		{
			var keys = new List<string>();

			_options.Keys.ToList().ForEach(keys.Add);

			if (keysOfDefaults && FormatSpecifier != null)
			{
				FormatSpecifier
					.Where(o => !_options.ContainsKey(o.Name) && o.DefaultValue != null)
					.ToList()
					.ForEach(o => keys.Add(o.Name));
			}

			return keys;
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