using System.Collections.Generic;
using Config.ConfigExceptions;

namespace Config.IniFiles.Validation
{
    internal sealed class RelaxedStrategy : ValidationStrategy
    {
        #region Overrides of ValidationStrategy

        /// <summary>
        ///     Validates the configuration if contains all required members.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns></returns>
        public override IList<ConfigException> ValidateConfig(IConfig config)
        {
            var errors = FoundMissingMembers(config);
            return errors;
        }

        #endregion
    }
}