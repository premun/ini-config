using System.Collections;
using System.Collections.Generic;
using Config.ConfigExceptions;
using Config.Format;
using Config.IniFiles.Errors;

namespace Config.IniFiles.Validation
{
    public interface IValidation
    {
        IList<ConfigException> ValidateConfig(IConfig congif);
    }

    public abstract class ValidationStrategy : IValidation {
        #region Implementation of IValidation

        public abstract IList<ConfigException> ValidateConfig(IConfig congif);

        #endregion
    }

    public class StrictStrategy : ValidationStrategy
    {
        #region Overrides of ValidationStrategy

        public override IList<ConfigException> ValidateConfig(IConfig congif)
        {
            var errors = new List<ConfigException>();

            var foundSection = new List<SectionSpecifier>();

            foreach (var section in congif.FormatSpecifier.Sections)
            {
                if (!congif.ContainSection(section.Name))
                {
                    errors.Add(new MissingSectionException(section.Name));
                }
                else
                {
                    foundSection.Add(section);
                    foreach (var requiredOption in section)
                    {
                        if (!congif[section.Name].Contain(requiredOption.Name))
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

    public class RelaxedStrategy : ValidationStrategy
    {
        #region Overrides of ValidationStrategy

        public override IList<ConfigException> ValidateConfig(IConfig congif)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}