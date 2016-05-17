using System.Collections.Generic;
using Config.ConfigExceptions;

namespace Config.IniFiles.Validation
{
    public interface IValidation
    {
        IList<ConfigException> ValidateConfig(IConfig config);
    }

    public abstract class ValidationStrategy : IValidation
    {
        #region Implementation of IValidation

        public abstract IList<ConfigException> ValidateConfig(IConfig config);

        protected List<ConfigException> FoundMissingMembers(IConfig config)
        {
            var errors = new List<ConfigException>();

            if (config.FormatSpecifier == null)
            {
                return errors;
            }

            foreach (var section in config.FormatSpecifier.Sections)
            {
                if (!config.ContainSection(section.Name) && section.Required)
                {
                    errors.Add(new MissingSectionException(section.Name));
                }
                else
                {
                    foreach (var requiredOption in section)
                    {
                        if (!config[section.Name].Contains(requiredOption.Name) && requiredOption.Required)
                        {
                            errors.Add(new MissingOptionException(section.Name, requiredOption.Name));
                        }
                    }
                }
            }

            return errors;
        }

        #endregion
    }
}