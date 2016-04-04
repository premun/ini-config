using System;

namespace Config.Attribute
{
	/// <summary>
	/// Class used for creating objects that inject config values through attributes.
	/// </summary>
	public static class ConfigFactory
    {
		/// <summary>
		/// Instantiates given type and injects values from config into annotated fields/properties.
		/// </summary>
		/// <typeparam name="T">Type we would like to instantiate.</typeparam>
		/// <returns>Instance of given type T.</returns>
        public static T Create<T>()
        {
            // Find all attributes and load data.
            throw new NotImplementedException();
        }
    }
}