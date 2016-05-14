using System.Collections;
using System.Collections.Generic;
using Config.ConfigExceptions;
using Config.Format;
using Config.IniFiles.Errors;

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

            foreach (var section in config.FormatSpecifier.Sections)
            {
                if (!config.ContainSection(section.Name))
                {
                    errors.Add(new MissingSectionException(section.Name));
                }
                else
                {
                    foreach (var requiredOption in section)
                    {
                        if (!config[section.Name].Contain(requiredOption.Name))
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
            foreach (var section in config.Sections)
            {
                if (!config.FormatSpecifier.Contain(section.Name))
                {
                    errors.Add(new RedundantSectionException(section.Name));
                }
                else
                {
                    foreach (var requiredOption in section.Keys())
                    {
                        if (!config[section.Name].Contain(requiredOption))
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