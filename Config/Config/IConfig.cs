using System.Collections.Generic;
using Config.Format;

namespace Config
{
    /// <summary>
    ///     Interface represents main data structure, where configuration is held through runtime.
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        ///     Format specifier that was used when config instance was built.
        /// </summary>
        ConfigFormatSpecifier FormatSpecifier { get; set; }

        /// <summary>
        ///     Retrieves config section by its name.
        /// </summary>
        /// <param name="name">Section name</param>
        /// <returns>Section, if found, null otherwise.</returns>
        IConfigSection this[string name] { get; }

        /// <summary>
        ///     Returns a collection of all registered config sections.
        /// </summary>
        IEnumerable<IConfigSection> Sections { get; }

        /// <summary>
        ///     Adds a config sections, effectively overwriting an old one when their Name matches.
        /// </summary>
        /// <param name="section">Section to be added</param>
        /// <returns>True, if some old section with the same name was overwritten.</returns>
        bool AddSection(IConfigSection section);

        /// <summary>
        ///     Adds a config sections, effectively overwriting an old one when their Name matches.
        /// </summary>
        /// <param name="name">Name of the new section</param>
        /// <returns>Either existing section with given name or a newly created one.</returns>
        IConfigSection AddSection(string name);

        /// <summary>
        ///     Removes given config sections.
        /// </summary>
        /// <param name="section">Section to be removed</param>
        /// <returns>True, if section was present.</returns>
        bool RemoveSection(IConfigSection section);

        /// <summary>
        ///     Removes given config sections.
        /// </summary>
        /// <param name="name">Section's name</param>
        /// <returns>True, if section was present.</returns>
        bool RemoveSection(string name);

        bool ContainSection(string name);
    }
}