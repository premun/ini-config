using System.Collections;
using System.Collections.Generic;
using Config.Format.OptionSpecifiers;

namespace Config.Format
{
    /// <summary>
    ///     Contains a specification of one config section. Each option of the section is represents by a OptionSpecifier.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IEnumerable{Config.Format.OptionSpecifiers.OptionSpecifier}" />
    internal class SectionSpecifier : IEnumerable<OptionSpecifier>
    {
        private readonly IDictionary<string, OptionSpecifier> _options;

        internal SectionSpecifier(string name, bool required = false)
        {
            Name = name;
            Required = required;
            _options = new Dictionary<string, OptionSpecifier>();
        }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        internal string Name { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this <see cref="SectionSpecifier" /> is required.
        /// </summary>
        /// <value>
        ///     <c>true</c> if required; otherwise, <c>false</c>.
        /// </value>
        internal bool Required { get; set; }

        /// <summary>
        ///     Gets or sets the <see cref="OptionSpecifier" /> with the specified key.
        ///     Get: If does not contains the option returns <c>null</c>.
        /// </summary>
        /// <value>
        ///     The <see cref="OptionSpecifier" />.
        /// </value>
        /// <param name="key">The key.</param>
        /// <returns></returns>
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

        public IEnumerator<OptionSpecifier> GetEnumerator()
        {
            return _options.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Adds the option.
        /// </summary>
        /// <param name="optionSpecifier">The option specifier.</param>
        internal void AddOption(OptionSpecifier optionSpecifier)
        {
            _options[optionSpecifier.Name] = optionSpecifier;
        }

        /// <summary>
        ///     Determines whether [contains] [the specified name] an option.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        internal bool Contains(string name)
        {
            return _options.ContainsKey(name);
        }
    }
}