using System.Collections.Generic;
using Config.Options;

namespace Config
{
	/// <summary>
	/// Represents data of one config section (list of key-value items).
	/// Also behaves like a collection of items.
	/// </summary>
	public interface IConfigSection : IEnumerable<Option>
    {
		/// <summary>
		/// Gets or sets value of an option.
		/// If value is not present and we try to retrieve it, 
		/// we check whether there was a default value set in the format specifier.
		/// </summary>
		/// <param name="key">Option key</param>
		/// <returns>Option with given key, or null when no option/default found.</returns>
		Option this[string key] { get; set; }

		/// <summary>
		/// Gets the section's name.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets or sets section's comment;
		/// </summary>
		string Comment { get; set; }

		/// <summary>
		/// Returns a list of keys of all options inside the config or of options with default values.
		/// </summary>
		/// <param name="keysOfDefaults">Indicates whether to also return keys of non-present options with default values</param>
		/// <returns>List of option keys</returns>
		IEnumerable<string> Keys(bool keysOfDefaults = true);

			/// <summary>
		/// Sets an item (key-value), effectively overwriting previous item with same key.
		/// Can be chained.
		/// </summary>
		/// <param name="key">Item key</param>
		/// <param name="value">Item value</param>
		/// <returns>Itself for better chaining.</returns>
		IConfigSection Set(string key, Option value);

		/// <summary>
		/// Removes an option item from section.
		/// </summary>
		/// <param name="key">Item's key</param>
		/// <returns>True, when item was present and removed, false otherwise.</returns>
		bool Remove(string key);
    }
}
