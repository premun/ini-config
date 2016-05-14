using System.Collections.Generic;
using Config.ConfigExceptions;

namespace Config.IniFiles.Validation
{
    public class RelaxedStrategy : ValidationStrategy
    {
        #region Overrides of ValidationStrategy

        public override IList<ConfigException> ValidateConfig(IConfig config)
        {
            var errors = FoundMissingMembers(config);
            return errors;
        }

        #endregion
    }
}