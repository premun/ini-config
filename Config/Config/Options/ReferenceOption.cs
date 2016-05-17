using System;
using System.Linq;
using Config.ConfigExceptions;

namespace Config.Options
{
    /// <summary>
    ///     Represents reference on other option.
    /// </summary>
    /// <seealso cref="Config.Options.Option" />
    public sealed class ReferenceOption : Option
    {
        private readonly IConfig _parrentConfig;

        private bool _cycleIndicator;

        public ReferenceOption(string sectionName, string optionName,
            IConfig config)
        {
            _parrentConfig = config;
            Section = sectionName;
            Option = optionName;
        }

        public string Section { get; set; }

        public string Option { get; set; }

        #region Overrides of Option

        /// <summary>
        ///     Gets the data from parent config by the reference.
        ///     Checks possible cycle of references.
        /// </summary>
        /// <value>
        ///     The data.
        /// </value>
        /// <exception cref="Config.ConfigExceptions.ReferenceCycleException"></exception>
        /// <exception cref="Config.ConfigExceptions.MissingReferencedException"></exception>
        /// <exception cref="System.InvalidOperationException">Cannot set value to reference option.</exception>
        public override object Data
        {
            get
            {
                if (_cycleIndicator)
                {
                    throw new ReferenceCycleException(Section, Option);
                }

                if (!_parrentConfig[Section].Keys().Contains(Option))
                {
                    throw new MissingReferencedException(Section, Option);
                }

                _cycleIndicator = true;
                var result = _parrentConfig[Section][Option].Data;
                _cycleIndicator = false;

                return result;
            }
            protected set
            {
                throw new InvalidOperationException(
                    "Cannot set value to reference option.");
            }
        }

        #endregion
    }
}