using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Config.Format;
using Config.Options;

namespace Config
{
    /// <summary>
    ///     Represents a section of config containing config options.
    /// </summary>
    public class ConfigSection : IConfigSection
    {
        private readonly Dictionary<string, Option> _options;

        public ConfigSection(string name)
        {
            Name = name;
            _options = new Dictionary<string, Option>();
        }

        internal SectionSpecifier FormatSpecifier { get; set; }

        public string Name { get; private set; }

        public string Comment { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="Option" /> with the specified key.
        ///     Checks if contains th option and returns it. Otherwise try to find default value or return <c>null</c>.
        /// </summary>
        /// <value>
        ///     The <see cref="Option" />.
        /// </value>
        /// <param name="key">The key.</param>
        /// <returns>Founded option / default value / null.</returns>
        /// <exception cref="System.InvalidOperationException">Cannot set value to reference option.</exception>
        public Option this[string key]
        {
            get
            {
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
                if (_options.ContainsKey(key) &&
                    _options[key].GetType() == typeof (ReferenceOption))
                {
                    throw new InvalidOperationException(
                        "Cannot set value to reference option.");
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
                    .Where(
                        o =>
                            !_options.ContainsKey(o.Name) &&
                            o.DefaultValue != null)
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

        public bool Contains(string key)
        {
            return _options.ContainsKey(key);
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