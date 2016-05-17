using System;

namespace Config
{
    /// <summary>
    ///     Represents functionality for storing IConfig objects.
    /// </summary>
    public interface IConfigSaver
    {
        /// <summary>
        ///     Saves given IConfig data. Target is dependent on implementation (ini file, database...).
        /// </summary>
        /// <param name="config">Config data object</param>
        /// <param name="verbosity">Verbosity of saver</param>
        void SaveConfig(IConfig config, Verbosity verbosity);
    }

    [Flags]
    public enum Verbosity
    {
        None = 0,
        Defaults = 1,
        Comments = 2
    }
}