using System.Collections.Generic;
using Config.ConfigExceptions;

namespace Config.IniFiles.Validation
{
    internal sealed class StrictStrategy : ValidationStrategy
    {
        #region Overrides of ValidationStrategy

        /// <summary>
        ///     Validates the configuration if contains all required members and does not contain anything else.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns>List of errors.</returns>
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

            // Checks all config sections
            foreach (var section in config.Sections)
            {
                // Section is redundant
                if (!config.FormatSpecifier.Contains(section.Name))
                {
                    errors.Add(new RedundantSectionException(section.Name));
                }
                else
                {
                    // Checks all section options
                    foreach (var requiredOption in section.Keys())
                    {
                        // Option is redundant
                        if (
                            !config.FormatSpecifier[section.Name].Contains(
                                requiredOption))
                        {
                            errors.Add(new RedundantOptionException(
                                section.Name, requiredOption));
                        }
                    }
                }
            }

            return errors;
        }
    }
}