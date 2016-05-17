using System;
using Config.Format;

namespace Config.IniFiles
{
    /// <summary>
    ///     Facade class for ini file manipulation. Sugarcoats using IConfig together with ini files.
    /// </summary>
    public static class IniConfig
    {
        private static string _lastPath;

        /// <summary>
        ///     Loads config from given file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="formatSpecifier">The format specifier.</param>
        /// <param name="buildMode">The build mode.</param>
        /// <returns></returns>
        public static IConfig FromFile(string path,
            ConfigFormatSpecifier formatSpecifier = null,
            BuildMode buildMode = BuildMode.Strict)
        {
            var config = new IniFileConfigBuilder(path).Build(formatSpecifier,
                buildMode);
            _lastPath = path;
            return config;
        }

        /// <summary>
        ///     Saves given config to file.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="path">The path.</param>
        /// <param name="verbosity">The verbosity.</param>
        public static void SaveToFile(this IConfig config, string path,
            Verbosity verbosity = Verbosity.Defaults)
        {
            new IniFileConfigSaver(path).SaveConfig(config, verbosity);
        }

        /// <summary>
        ///     Saves given config to file.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="verbosity">The verbosity.</param>
        /// <exception cref="System.InvalidOperationException">Please specify the file name</exception>
        public static void SaveToFile(this IConfig config,
            Verbosity verbosity = Verbosity.Defaults)
        {
            if (string.IsNullOrEmpty(_lastPath))
            {
                throw new InvalidOperationException(
                    "Please specify the file name");
            }

            new IniFileConfigSaver(_lastPath).SaveConfig(config, verbosity);
        }
    }
}