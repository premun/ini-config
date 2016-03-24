using Config.Format;

namespace Config
{
    /// <summary>
    /// Represents functionality for creating IConfig objects following supplied structure/format.
    /// Data source is dependent on concrete implementation (ini file, database...).
    /// </summary>
	public interface IConfigBuilder
	{
        /// <summary>
        /// Builds an IConfig instance. Depending on supplied format requirements and build mode generates exception.
        /// </summary>
        /// <param name="formatSpecifier">Specification of desired config structure</param>
        /// <param name="buildMode">Should an exception be raised when format doesn't correspond with the specified structure?</param>
        /// <returns>Built IConfig filled with data</returns>
        /// <exception cref="ConfigFormatException">Thrown when config format does not correspond with supplied specification.</exception>
		IConfig Build(IFormatSpecifier formatSpecifier = null, BuildMode buildMode = BuildMode.Relaxed);

        /// <summary>
        /// Generates an empty config that can be filled with values manually.
        /// </summary>
		IConfig Empty { get; }
	}
}