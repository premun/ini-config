using System.Collections.Generic;
using Config.ConfigExceptions;

namespace Config.IniFiles.Validation
{
    public class StrictStrategy : ValidationStrategy
    {
        #region Overrides of ValidationStrategy

        public override IList<ConfigException> ValidateConfig(IConfig config)
        {
            var errors = FoundMissingMembers(config);
            errors.AddRange(CheckRedundantMembers(config));
            return errors;
        }

        #endregion

        private IList<ConfigException> CheckRedundantMembers(IConfig config)
        {
            var errors = new List<ConfigException>();

            if (config.FormatSpecifier == null)
            {
                return errors;
            }

            foreach (var section in config.Sections)
            {
                if (!config.FormatSpecifier.Contains(section.Name))
                {
                    errors.Add(new RedundantSectionException(section.Name));
                }
                else
                {
                    foreach (var requiredOption in section.Keys())
                    {
                        if (!config.FormatSpecifier[section.Name].Contains(requiredOption))
                        {
                            errors.Add(new RedundantOptionException(section.Name, requiredOption));
                        }
                    }
                }
            }

            return errors;
        }
    }
}