using System.Collections.Generic;
using Config.Options;

namespace Config
{
	/// <summary>
	/// Represents data of one config section (list of key-value items).
	/// Also behaves like a collection of items.
	/// </summary>
	public interface IConfigSection : IDictionary<string, Option>
    {
        /// <summary>
        /// Unique name of the config section, used for identification.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; }

        /// <summary>
        /// Gets or sets the description of the section about its options.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        string Comment { get; set; }

        /// <summary>
        /// Sets an item (key-value), effectively overwriting previous item with same key.
        /// </summary>
        /// <param name="key">Item key</param>
        /// <param name="value">Item value</param>
        /// <returns>Itself for better chaining.</returns>
        IConfigSection Set(string key, Option value);
    }
}
