using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ConfigTests")]

namespace Config.Format
{
    /// <summary>
    ///     Specifies the structure of configuration file. The specifier contains all section specifications.
    /// </summary>
    public class ConfigFormatSpecifier
    {
        private readonly IDictionary<string, SectionSpecifier> _sections;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConfigFormatSpecifier" /> class.
        /// </summary>
        public ConfigFormatSpecifier()
        {
            _sections = new Dictionary<string, SectionSpecifier>();
        }

        /// <summary>
        ///     Gets or sets the <see cref="SectionSpecifier" /> with the specified key.
        /// </summary>
        /// <value>
        ///     The <see cref="SectionSpecifier" />.
        /// </value>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        internal SectionSpecifier this[string key]
        {
            get
            {
                if (_sections.ContainsKey(key))
                {
                    return _sections[key];
                }

                return null;
            }

            set { _sections[key] = value; }
        }

        /// <summary>
        ///     Gets the all sections of the config.
        /// </summary>
        /// <value>
        ///     The sections.
        /// </value>
        internal IEnumerable<SectionSpecifier> Sections
        {
            get { return _sections.Values; }
        }

        /// <summary>
        ///     Determines whether [contains] [the specified key].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>if contains <c>true</c> otherwise <c>false</c>.</returns>
        public bool Contains(string key)
        {
            return _sections.ContainsKey(key);
        }

        /// <summary>
        ///     Adds the section to the creating config format.
        /// </summary>
        /// <param name="name">The name of the section.</param>
        /// <param name="required">if set to <c>true</c> [required].</param>
        /// <returns>Fluent object with config specification.</returns>
        public FluentSectionSpecifier AddSection(string name,
            bool required = false)
        {
            var sectionSpecifier = new SectionSpecifier(name, required);
            _sections[name] = sectionSpecifier;
            return new FluentSectionSpecifier(this, sectionSpecifier);
        }
    }
}