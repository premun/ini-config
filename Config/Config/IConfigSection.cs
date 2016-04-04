using System.Collections.Generic;
using Config.Values;

namespace Config
{
    /// <summary>
    /// Represents data of one config section (list of key-value items).
    /// Also behaves like a collection of items.
    /// </summary>
    /// TODO: Name a Required bych dal readonly setovane v constructoru.
    /// TODO: Zrusit IDictionary? Protoze napriklad moc nechceme mit setter na indexeru.
    /// TODO: Minimalne setter musime prepsat, aby do nej sly ukladat vsechny mozny hodnoty a ono to z nich delalo ConfigValue.
    public interface IConfigSection : IDictionary<string, ConfigValue>
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
        string Description { get; set; }

        /// <summary>
        /// TODO: exception type to doc
        /// Returns a single item by given key. 
        /// An [exception] will be raised when object is not of T type.
        /// </summary>
        /// <typeparam name="T">Desired item type. Type is cast silently.</typeparam>
        /// <param name="key">Item key</param>
        /// <returns>Item, if found and good type is specified, null otherwise.</returns>
        T Get<T>(string key);

        /// <summary>
        /// Returns a single item by given key. 
        /// Type is cast silently, null returned in case it fails.
        /// </summary>
        /// <typeparam name="T">Desired item type. Type is cast silently.</typeparam>
        /// <param name="key">Item key</param>
        /// <returns>Item, if found and good type is specified, null otherwise.</returns>
        T GetSafe<T>(string key); 

        /// <summary>
        /// Sets an item (key-value), effectively overwriting previous item with same key.
        /// </summary>
        /// <param name="key">Item key</param>
        /// <param name="value">Item value</param>
        /// <returns>Itself for better chaining.</returns>
        IConfigSection Set(string key, object value);
    }
}
