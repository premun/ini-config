using System.Collections.Generic;
using Config.Format;
using Config.IniFiles.Errors;

namespace Config
{
    /// <summary>
    ///     Represents functionality for creating IConfig objects following supplied structure/format.
    ///     Data source is dependent on concrete implementation (ini file, database...).
    /// </summary>
    public interface IConfigBuilder
    {
        /// <summary>
        ///     Gets whether building the config was successful.
        /// </summary>
        bool Ok { get; }

        /// <summary>
        ///     Gets a list of errors that occured during building.
        /// </summary>
        IEnumerable<FormatError> Errors { get; }

        /// <summary>
        ///     Builds an IConfig instance. Depending on supplied format requirements and build mode generates exception.
        /// </summary>
        /// <param name="formatSpecifier">Specification of desired config structure</param>
        /// <param name="buildMode">Should an exception be raised when format doesn't correspond with the specified structure?</param>
        /// <returns>Built IConfig filled with data</returns>
        /// <exception cref="ConfigFormatException">Thrown when config format does not correspond with supplied specification.</exception>
        IConfig Build(ConfigFormatSpecifier formatSpecifier = null,
            BuildMode buildMode = BuildMode.Relaxed);

        /// <summary>
        ///     Builds an IConfig instance. Depending on supplied format requirements and build mode generates exception.
        /// </summary>
        /// <param name="config">Config object where values will be built to</param>
        /// <param name="formatSpecifier">Specification of desired config structure</param>
        /// <param name="buildMode">Should an exception be raised when format doesn't correspond with the specified structure?</param>
        /// <exception cref="ConfigFormatException">Thrown when config format does not correspond with supplied specification.</exception>
        void Build(IConfig config, ConfigFormatSpecifier formatSpecifier = null,
            BuildMode buildMode = BuildMode.Relaxed);
    }
}