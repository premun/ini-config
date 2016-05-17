using System.Collections.Generic;
using Config.ConfigExceptions;

namespace Config.IniFiles.Validation
{
    internal abstract class ValidationStrategy : IValidation
    {
        #region Implementation of IValidation

        public abstract IList<ConfigException> ValidateConfig(IConfig config);

        /// <summary>
        ///     Founds the missing members at validating config.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns>List of errors.</returns>
        protected List<ConfigException> FoundMissingMembers(IConfig config)
        {
            var errors = new List<ConfigException>();

            if (config.FormatSpecifier == null)
            {
                return errors;
            }

            // Checks all specified sections at formatter
            foreach (var section in config.FormatSpecifier.Sections)
            {
                // Missing section
                if (!config.ContainSection(section.Name) && section.Required)
                {
                    errors.Add(new MissingSectionException(section.Name));
                }
                else
                {
                    // Checks all specified options at formatter
                    foreach (var requiredOption in section)
                    {
                        // Missing option
                        if (
                            !config[section.Name].Contains(requiredOption.Name) &&
                            requiredOption.Required)
                        {
                            errors.Add(new MissingOptionException(section.Name,
                                requiredOption.Name));
                        }
                    }
                }
            }

            return errors;
        }

        #endregion
    }
}