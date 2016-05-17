using Config.Format.OptionSpecifiers;

namespace Config.Format
{
    /// <summary>
    ///     Allows fluent specification of the config. For more information about pattern:
    ///     <see href="https://en.wikipedia.org/wiki/Fluent_interface" />
    /// </summary>
    public class FluentSectionSpecifier
    {
        private readonly ConfigFormatSpecifier _parentSpecifier;
        private readonly SectionSpecifier _sectionSpecifier;

        internal FluentSectionSpecifier(ConfigFormatSpecifier parentSpecifier,
            SectionSpecifier sectionSpecifier)
        {
            _parentSpecifier = parentSpecifier;
            _sectionSpecifier = sectionSpecifier;
        }

        /// <summary>
        ///     Adds the section.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="required">if set to <c>true</c> [required].</param>
        /// <returns>Fluent specifier with new section.</returns>
        public FluentSectionSpecifier AddSection(string name,
            bool required = false)
        {
            return _parentSpecifier.AddSection(name, required);
        }

        /// <summary>
        ///     Adds the option to the last section.
        /// </summary>
        /// <param name="optionSpecifier">The option specifier.</param>
        /// <returns>Fluent specifier with new option.</returns>
        public FluentSectionSpecifier AddOption(OptionSpecifier optionSpecifier)
        {
            _sectionSpecifier.AddOption(optionSpecifier);
            return this;
        }

        /// <summary>
        ///     Finishes the definition of the config.
        /// </summary>
        /// <returns>Prepared format specifier.</returns>
        public ConfigFormatSpecifier FinishDefinition()
        {
            return _parentSpecifier;
        }
    }
}