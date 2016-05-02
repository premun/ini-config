using System.Collections;
using System.Collections.Generic;
using Config.Format.OptionSpecifiers;

namespace Config.Format
{
	internal class SectionSpecifier : IEnumerable<OptionSpecifier>
	{
		private readonly IDictionary<string, OptionSpecifier> _options;

		internal string Name { get; set; }

		internal bool Required { get; set; }

		internal OptionSpecifier this[string key]
		{
			get
			{
				if (_options.ContainsKey(key))
				{
					return _options[key];
				}

				return null;
			}

			set { _options[key] = value; }
		}

		internal SectionSpecifier(string name, bool required = false)
		{
			Name = name;
			Required = required;
			_options = new Dictionary<string, OptionSpecifier>();
		}

		internal void AddOption(OptionSpecifier optionSpecifier)
		{
			_options[optionSpecifier.Name] = optionSpecifier;
		}

		public IEnumerator<OptionSpecifier> GetEnumerator()
		{
			return _options.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}