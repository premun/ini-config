using System.Collections.Generic;

namespace Config
{
    /// <summary>
    /// Interface represents main data structure, where configuration is held through runtime.
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        /// Retreive config section by its name.
        /// </summary>
        /// <param name="sectionName">Section name</param>
        /// <returns>Section, if found, null otherwise.</returns>
        IConfigSection this[string sectionName] { get; }

        /// <summary>
        /// Retreive config section by its name.
        /// </summary>
        /// <param name="sectionName">Section name</param>
        /// <returns>Section, if found, null otherwise.</returns>
        IConfigSection GetSection(string sectionName);

        /// <summary>
        /// Returns a collection of all registered config sections.
        /// </summary>
		IEnumerable<IConfigSection> Sections { get; }

		/// <summary>
		/// Adds a config sections, effectively overwriting an old one when their Name matches.
		/// </summary>
		/// <param name="section">Section to be added</param>
		/// <returns>True, if some old section with the same name was overwritten.</returns>
		bool AddSection(IConfigSection section);

		/// <summary>
		/// Adds a config sections, effectively overwriting an old one when their Name matches.
		/// </summary>
		/// <param name="name">Name of the new section</param>
		/// <returns>Either existing section with given name or a newly created one.</returns>
		IConfigSection AddSection(string name);

		/// <summary>
		/// Removes given config sections.
		/// </summary>
		/// <param name="section">Section to be removed</param>
		/// <returns>True, if section was present.</returns>
		bool RemoveSection(IConfigSection section);

        /// <summary>
        /// Removes given config sections.
        /// </summary>
        /// <param name="sectionName">Section's name</param>
        /// <returns>True, if section was present.</returns>
        bool RemoveSection(string sectionName);
    }
}