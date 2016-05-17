using System.Collections.Generic;
using Config.ConfigExceptions;

namespace Config.IniFiles.Validation
{
    internal interface IValidation
    {
        /// <summary>
        /// Validates the configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns>The list of errors.</returns>
        IList<ConfigException> ValidateConfig(IConfig config);
    }
}